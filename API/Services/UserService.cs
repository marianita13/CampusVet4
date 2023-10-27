using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Api.Helpers;
using API.Dtos;
using API.Helpers;
using Domain.entities;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ApiJwt.Services;

public class ClientService : IUserService
{
    private readonly CampusVet4 _CampusVet;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher<Client> _passwordHasher;
    public ClientService(IUnitOfWork unitOfWork, IOptions<CampusVet4> Ca_CampusVet, IPasswordHasher<Client> passwordHasher)
    {
        _CampusVet = Ca_CampusVet.Value;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
    }

    public async Task<string> RegisterAsync(RegisterDto registerDto)
    {
        var Client = new Client
        {
            Email = registerDto.Email,
            ClientName = registerDto.Clientname
        };

        Client.Password = _passwordHasher.HashPassword(Client, registerDto.Password); //Encrypt password

        var existingClient = _unitOfWork.Clients
                                    .Find(u => u.ClientName.ToLower() == registerDto.Clientname.ToLower())
                                    .FirstOrDefault();

        if (existingClient == null)
        {
            var rolDefault = _unitOfWork.Roles
                                    .Find(u => u.Name == Authorization.rol_default.ToString())
                                    .First();
            try
            {
                Client.Rols.Add(rolDefault);
                _unitOfWork.Clients.Add(Client);
                await _unitOfWork.SaveAsync();

                return $"Client  {registerDto.Clientname} has been registered successfully";
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                return $"Error: {message}";
            }
        }
        else
        {
            return $"Client {registerDto.Clientname} already registered.";
        }
    }

    public async Task<DataClientDto> GetTokenAsync(LoginDto model)
    {
        DataClientDto DataClientDto = new DataClientDto();
        var Client = await _unitOfWork.Clients
        .GetByClientnameAsync(model.Clientname);

        if (Client == null)
        {
            DataClientDto.IsAuthenticated = false;
            DataClientDto.Message = $"Client does not exist with Clientname {model.Clientname}.";
            return DataClientDto;
        }

        var result = _passwordHasher.VerifyHashedPassword(Client, Client.Password, model.Password);

        if (result == PasswordVerificationResult.Success)
        {
            DataClientDto.IsAuthenticated = true;
            JwtSecurityToken jwtSecurityToken = CreateJwtToken(Client);
            DataClientDto.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            DataClientDto.Email = Client.Email;
            DataClientDto.ClientName = Client.ClientName;
            DataClientDto.Roles = Client.Rols
                                            .Select(u => u.Name)
                                            .ToList();

            if (Client.RefreshTokens.Any(a => a.IsActive))
            {
                var activeRefreshToken = Client.RefreshTokens.Where(a => a.IsActive == true).FirstOrDefault();
                DataClientDto.RefreshToken = activeRefreshToken.Token;
                DataClientDto.RefreshTokenExpiration = activeRefreshToken.Expires;
            }
            else
            {
                var refreshToken = CreateRefreshToken();
                DataClientDto.RefreshToken = refreshToken.Token;
                DataClientDto.RefreshTokenExpiration = refreshToken.Expires;
                Client.RefreshTokens.Add(refreshToken);
                _unitOfWork.Clients.Update(Client);
                await _unitOfWork.SaveAsync();
            }

            return DataClientDto;
        }
        DataClientDto.IsAuthenticated = false;
        DataClientDto.Message = $"Credenciales incorrectas para el usuario {Client.ClientName}.";
        return DataClientDto;
    }

    public async Task<DataClientDto> RefreshTokenAsync(string refreshToken)
    {
        var DataClientDto = new DataClientDto();

        var usuario = await _unitOfWork.Clients
        .GetByRefreshTokenAsync(refreshToken);

        if (usuario == null)
        {
            DataClientDto.IsAuthenticated = false;
            DataClientDto.Message = $"Token is not assigned to any Client.";
            return DataClientDto;
        }

        var refreshTokenBd = usuario.RefreshTokens.Single(x => x.Token == refreshToken);

        if (!refreshTokenBd.IsActive)
        {
            DataClientDto.IsAuthenticated = false;
            DataClientDto.Message = $"Token is not active.";
            return DataClientDto;
        }
        //Revoque the current refresh token and
        refreshTokenBd.Revoked = DateTime.UtcNow;
        //generate a new refresh token and save it in the database
        var newRefreshToken = CreateRefreshToken();
        usuario.RefreshTokens.Add(newRefreshToken);
        _unitOfWork.Clients.Update(usuario);
        await _unitOfWork.SaveAsync();
        //Generate a new Json Web Token
        DataClientDto.IsAuthenticated = true;
        JwtSecurityToken jwtSecurityToken = CreateJwtToken(usuario);
        DataClientDto.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        DataClientDto.Email = usuario.Email;
        DataClientDto.ClientName = usuario.ClientName;
        DataClientDto.Roles = usuario.Rols
        .Select(u => u.Name)
        .ToList();
        DataClientDto.RefreshToken = newRefreshToken.Token;
        DataClientDto.RefreshTokenExpiration = newRefreshToken.Expires;
        return DataClientDto;
    }
    private RefreshToken CreateRefreshToken()
    {
        var randomNumber = new byte[32];
        using (var generator = RandomNumberGenerator.Create())
        {
            generator.GetBytes(randomNumber);
            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomNumber),
                Expires = DateTime.UtcNow.AddDays(10),
                Created = DateTime.UtcNow
            };
        }
    }
    private JwtSecurityToken CreateJwtToken(Client usuario)
    {
        var roles = usuario.Rols;
        var roleClaims = new List<Claim>();
        foreach (var role in roles)
        {
            roleClaims.Add(new Claim("roles", role.Name));
        }
        var claims = new[]
        {
                                new Claim(JwtRegisteredClaimNames.Sub, usuario.ClientName),
                                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                                new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
                                new Claim("uid", usuario.Id.ToString())
                        }
        .Union(roleClaims);
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_CampusVet.Key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _CampusVet.Issuer,
            audience: _CampusVet.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_CampusVet.DurationInMinutes),
            signingCredentials: signingCredentials);
        return jwtSecurityToken;
    }

    public async Task<string> AddRoleAsync(AddRoleDto model)
    {
        {

        var Client = await _unitOfWork.Clients
        .GetByClientnameAsync(model.Clientname);
        if (Client == null)
        {
            return $"Client {model.Clientname} does not exists.";
        }

        var result = _passwordHasher.VerifyHashedPassword(Client, Client.Password, model.Password);

        if (result == PasswordVerificationResult.Success)
        {
            var rolExists = _unitOfWork.Roles
                                        .Find(u => u.Name.ToLower() == model.Role.ToLower())
                                        .FirstOrDefault();

            if (rolExists != null)
            {
                var ClientHasRole = Client.Rols
                                            .Any(u => u.Id == rolExists.Id);

                if (ClientHasRole == false)
                {
                    Client.Rols.Add(rolExists);
                    _unitOfWork.Clients.Update(Client);
                    await _unitOfWork.SaveAsync();
                }

                return $"Role {model.Role} added to Client {model.Clientname} successfully.";
            }

            return $"Role {model.Role} was not found.";
        }
        return $"Invalid Credentials";
    }
    }
}
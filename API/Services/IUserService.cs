using API.Dtos;
using Domain.entities;

namespace ApiJwt.Services;

public interface IUserService
{
    Task<string> RegisterAsync(RegisterDto model);
    Task<DataClientDto> GetTokenAsync(LoginDto model);
    Task<string> AddRoleAsync(AddRoleDto model);
    Task<DataClientDto> RefreshTokenAsync(string refreshToken);
}
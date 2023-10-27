using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
public class ClientRepository : GenericRepository<Client> , IClient
{
    private readonly CampusVet4Context _context;
    public ClientRepository(CampusVet4Context context) : base(context)
    {
        _context = context;
    }

    public async Task<Client> GetByRefreshTokenAsync(string refreshToken)
    {
        return await _context.Clients
            .Include(u => u.Rols)
            .Include(u => u.RefreshTokens)
            .FirstOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == refreshToken));
    }

    public async Task<Client> GetByClientnameAsync(string Clientname)
    {
        return await _context.Clients
            .Include(u => u.Rols)
            .Include(u => u.RefreshTokens)
            .FirstOrDefaultAsync(u => u.ClientName.ToLower() == Clientname.ToLower());
    }

    public Task<Client> GetByUsernameAsync(string username)
    {
        throw new NotImplementedException();
    }
}
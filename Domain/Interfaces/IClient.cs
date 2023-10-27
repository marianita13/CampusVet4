using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.entities;

namespace Domain.Interfaces;

public interface IClient : IGenericRepository<Client>
{
    Task<Client> GetByClientnameAsync(string Clientname);
    Task<Client> GetByRefreshTokenAsync(string Clientname);
}
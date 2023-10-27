using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository
{
    public class RoleRepository: GenericRepository<Role>, IRole
{
    private readonly CampusVet4Context _context;

    public RoleRepository(CampusVet4Context context) : base(context)
    {
       _context = context;
    }
}
}
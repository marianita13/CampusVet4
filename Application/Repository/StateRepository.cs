using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;
public class StateRepository : GenericRepository<State> , IState
{
    private readonly CampusVet4Context _context;
    public StateRepository(CampusVet4Context context) : base(context)
    {
        _context = context;
    }
}
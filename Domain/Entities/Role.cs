using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.entities;

namespace Domain.Entities
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Client> Clients { get; set; } = new HashSet<Client>();
        public ICollection<ClientRol> ClientRols { get; set; }
    }
}
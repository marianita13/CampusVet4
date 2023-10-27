using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.entities;

namespace Domain.Entities
{
    public class ClientRol : BaseEntity
    {
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public int RoleId { get; set; }
        public Rol Rol { get; set; }
    }
}
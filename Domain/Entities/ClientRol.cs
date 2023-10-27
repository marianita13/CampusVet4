using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.entities
{
    public class ClientRol : BaseEntity
    {
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public int RoleId { get; set; }
        public Role Rol { get; set; }
    }
}
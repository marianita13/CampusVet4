using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.entities
{
    public class Client : BaseEntity
    {
        [Required]
        public string ClientName { get; set; }

        [Required]
        public string LastNames { get; set; }

        [Required]
        public string Email { get; set; }
        public string Password { get; set; }
        public ClientAddress ClientAddress { get; set; }
        public ICollection<ClientPhone> ClientPhone { get; set; }         
        public ICollection<Pet> Pets { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<Role> Rols { get; set; }
        public ICollection<RefreshToken> RefreshTokens { get; set; } = new HashSet<RefreshToken>();
        public ICollection<ClientRol> ClientRols { get; set; }
    }
}
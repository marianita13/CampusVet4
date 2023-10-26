using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.entities
{
    public class ClientPhone : BaseEntity
    {
        [Required]
        public int ClientId { get; set; }
        public Client Clients { get; set; }

        [Required]
        public string Number { get; set; }
    }
}

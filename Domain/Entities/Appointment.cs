using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.entities
{
    public class Appointment : BaseEntity
    {
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public TimeSpan Time { get; set; }
        
        [Required]
        public int ClientId { get; set; }
        public Client Clients { get; set; }

        [Required]
        public int PetId { get; set; }
        public Pet Pets { get; set; }

        [Required]
        public int ServiceId { get; set; }
        public Service Services { get; set; }
    }
}
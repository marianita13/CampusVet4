using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.entities
{
    public class Pet : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Kind { get; set; }

        [Required]
        public int BreedId { get; set; }
        public Breed Breed { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public int ClientId { get; set; }
        public Client Clients { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }
}
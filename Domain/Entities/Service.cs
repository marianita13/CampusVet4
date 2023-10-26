using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.entities
{
    public class Service : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public double Price { get; set; }
        public ICollection<Appointment> Appointments { get; set; } 
    }
}
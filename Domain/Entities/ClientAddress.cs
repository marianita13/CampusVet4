using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.entities
{
    public class ClientAddress : BaseEntity
    {
        [Required]
        public int ClientId { get; set; }
        public Client Clients { get; set; }


        public string TypeOfStreet { get; set; }
        public int FirstNumber { get; set; }
        public string Letter { get; set; }
        public string Bis { get; set; }
        public string SecondLetter { get; set; }
        public string Cardinal { get; set; }
        public int SecondNumber { get; set; }
        public string ThirdLetter { get; set; }
        public int ThirdNumber { get; set; }
        public string SecondCardinal { get; set; }
        public string Complement { get; set; }
        public string ZipCode { get; set; }

        [Required]
        public int IdCity { get; set; }
        public City Cities { get; set; }
    }
}
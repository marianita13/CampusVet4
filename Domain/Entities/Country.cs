using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.entities
{
    public class Country : BaseEntity
    {
        public string CountryName {get; set;}
        public ICollection<State> States { get; set; } 
    }
}
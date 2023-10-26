using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.entities
{
    public class Breed : BaseEntity
    {
        public string NameBreed { get; set; }
        public ICollection<Pet> Pets { get; set; }
    }
}
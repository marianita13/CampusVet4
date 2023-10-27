using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.entities
{
    public class City : BaseEntity
    {
        public string CityName { get; set; }
        public int StateId { get; set; }
        public State States { get; set; }
        public ICollection<Client> Clients { get; set; }
        public ClientAddress ClientAddresses { get; set; }
    }
}
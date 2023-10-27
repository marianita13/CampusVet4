using API.Controllers;
using Domain.entities;

namespace API.Dtos
{
    public class CityDto : BaseController
    {
        public int Id {get; set;}
        public string CityName { get; set; }
        public int StateId { get; set; }
        public State States { get; set; }
        public ICollection<Client> Clients { get; set; }
        public ClientAddress ClientAddresses { get; set; }
    }
}
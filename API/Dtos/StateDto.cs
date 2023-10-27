using API.Controllers;
using Domain.entities;

namespace API.Dtos
{
    public class StateDto : BaseController
    {
        public int Id {get; set;}
        public string StateName { get; set; }
        public int CountryId { get; set; }
        public ICollection<City> Cities { get; set; }
    }
}

using API.Controllers;
using Domain.entities;

namespace API.Dtos
{
    public class CountryDto : BaseController
    {
        public int Id {get; set;}
        public string CountryName {get; set;}
        public ICollection<State> States { get; set; }
    }
}
using API.Controllers;
using Domain.entities;

namespace API.Dtos
{
    public class BreedDto : BaseController
    {
        public int Id {get; set;}
        public string NameBreed { get; set; }
        public ICollection<Pet> Pets { get; set; }
    }
}
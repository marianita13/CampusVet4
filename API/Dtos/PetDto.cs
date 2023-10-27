using API.Controllers;
using Domain.entities;

namespace API.Dtos
{
    public class PetDto : BaseController
    {
        public int Id {get; set;}
        public string Name { get; set; }
        public string Kind { get; set;} 
        public int BreedId { get; set; }
        public DateTime BirthDate { get; set; }
        public int ClientId { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }
}
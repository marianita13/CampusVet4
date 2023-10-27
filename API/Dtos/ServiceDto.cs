using API.Controllers;
using Domain.entities;

namespace API.Dtos
{
    public class ServiceDto : BaseController
    {
        public int Id {get; set;}
        public string Name { get; set; }
        public double Price { get; set; }
        public ICollection<Appointment> Appointments { get; set; } 
    }
}
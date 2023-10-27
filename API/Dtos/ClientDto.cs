using System.Text.Json.Serialization;
using API.Controllers;
using Domain.entities;

namespace API.Dtos
{
    public class ClientDto : BaseController
    {
        public int Id {get; set;}
        public string ClientName { get; set; }
        public string LastNames { get; set; }
        public string Email { get; set; }
        public ClientAddress ClientAddress { get; set; }
        public ICollection<ClientPhone> ClientPhone { get; set; }         
        public ICollection<Pet> Pets { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }
}
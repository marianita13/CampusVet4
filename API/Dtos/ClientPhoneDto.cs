using API.Controllers;
using Domain.entities;

namespace API.Dtos
{
    public class ClientPhoneDto : BaseController
    {
        public int Id {get; set;}
        public int ClientId { get; set; }
        public string Number { get; set; }
    }
}
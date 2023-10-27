using API.Controllers;

namespace API.Dtos
{
    public class ClientAddressDto : BaseController
    {
        public int Id {get; set;}
        public int ClientId { get; set; }
        public string TypeOfStreet { get; set; }
        public int FirstNumber { get; set; }
        public string Letter { get; set; }
        public int SecondNumber { get; set; }
        public int ThirdNumber { get; set; }
        public string ZipCode { get; set; }
        public int IdCity { get; set; }
    }
}
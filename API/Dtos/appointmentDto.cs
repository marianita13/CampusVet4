using API.Controllers;

namespace API.Dtos
{
    public class AppointmentDto : BaseController
    {
        public int Id {get; set;}
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public int ClientId { get; set; }
        public int PetId { get; set; }
        public int ServiceId { get; set; }
    }
}
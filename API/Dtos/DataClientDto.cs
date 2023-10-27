using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class DataClientDto
    {
        public string Message { get; set; }
        public bool IsAuthenticated { get; set; }
        public string ClientName { get; set; }
        public string LastNames { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
        public string Token { get; set; }

        [JsonIgnore] // ->this attribute restricts the property to be shown in the result
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }
    }
}
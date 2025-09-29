using HMAS.Helper;
using System.Text.Json.Serialization;

namespace HMAS.DTO.Auth
{
    public class RegisterDTO
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public UserRole Role { get; set; }
    }
}

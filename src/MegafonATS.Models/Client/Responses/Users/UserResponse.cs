using MegafonATS.Models.Enums;
using System.Text.Json.Serialization;

namespace MegafonATS.Models.Client.Responses.Users
{
    public class UserResponse
    {
        [JsonPropertyName("ext")]
        public string UserExt { get; set; }
        [JsonPropertyName("telnum")]
        public string UserPhone { get; set; }
        [JsonPropertyName("mobile")]
        public string MobilePhone { get; set; }
        public string Login { get; set; }
        public string Position { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public UserRole Role { get; set; }
        public MobileRedirect MobileRedirect { get; set; }
    }

    public class MobileRedirect
    {
        public bool Enabled { get; set; }
        public bool Forward { get; set; }
        public int Delay { get; set; }
    }
}
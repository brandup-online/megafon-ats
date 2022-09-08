using System.Text.Json.Serialization;

namespace MegafonATS.Models.Client
{
    public class AccountModel
    {
        public string Name { get; set; }

        public string RealName { get; set; }

        [JsonPropertyName("ext")]
        public string UserExt { get; set; }

        [JsonPropertyName("telnum")]
        public string UserPhone { get; set; }
    }
}
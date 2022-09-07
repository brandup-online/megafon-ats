using System.Text.Json.Serialization;

namespace MegafonATS.Models.Client
{
    public class CurrentCallModel
    {
        [JsonPropertyName("CallID")]
        public string CallId { get; set; }
    }
}

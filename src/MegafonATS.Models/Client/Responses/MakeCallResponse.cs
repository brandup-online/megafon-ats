using System.Text.Json.Serialization;

namespace MegafonATS.Models.Client.Responses
{
    public class MakeCallResponse
    {
        [JsonPropertyName("callid")]

        public string CallId { get; set; }
    }
}

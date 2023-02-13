using System.Text.Json.Serialization;

namespace MegafonATS.Client.Models.Responses
{
    public class MakeCallResponse
    {
        [JsonPropertyName("callid")]

        public string CallId { get; set; }
    }
}

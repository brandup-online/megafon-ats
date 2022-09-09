using System.Text.Json.Serialization;

namespace MegafonATS.Models.Client.Requests
{
    public class ChangeUsersInGroupRequest : IRequestModel
    {
        [JsonPropertyName("add")]
        public string[] AddUsers { get; set; }
        public string[] CallsDisable { get; set; }
        public string[] Remove { get; set; }
        public int Position { get; set; } = -1;
    }
}

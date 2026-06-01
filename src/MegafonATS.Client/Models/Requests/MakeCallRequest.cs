using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MegafonATS.Client.Models.Requests
{
    public class MakeCallRequest : IRequestModel
    {
        [Required]
        public string Phone { get; set; }

        public string User { get; set; }

        public string Group { get; set; }

        public string Clid { get; set; }

        [JsonPropertyName("show_phone")]
        public bool? ShowPhone { get; set; }
    }
}
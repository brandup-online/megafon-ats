using System.ComponentModel.DataAnnotations;

namespace MegafonATS.Models.Client.Requests
{
    public class MakeCallRequest : IRequestModel
    {
        [Required]
        public string Phone { get; set; }
        [Required]
        public string User { get; set; }
    }
}

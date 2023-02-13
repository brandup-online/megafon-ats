using System.ComponentModel.DataAnnotations;

namespace MegafonATS.Client.Models.Requests
{
    public class MakeCallRequest : IRequestModel
    {
        [Required]
        public string User { get; set; }
        [Required]
        public string Phone { get; set; }
    }
}
using MegafonATS.Models.Client.Responses;
using MegafonATS.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace MegafonATS.Models.Client.Requests
{
    public class AddUserRequest
    {
        public string Login { get; set; }
        [Required]
        public string Name { get; set; }
        public string Password { get; set; }
        public string Position { get; set; }
        public string Email { get; set; }
        public string UserExt { get; set; }
        public UserRole Role { get; set; }
        public string Mobile { get; set; }
        public MobileRedirect MobileRedirect { get; set; }
    }
}

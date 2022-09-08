using MegafonATS.Models.Enums;

namespace MegafonATS.Models.Client.Responses
{
    public class UserResponse
    {
        public string Login { get; set; }
        public string Position { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string UserExt { get; set; }
        public string UserPhone { get; set; }
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
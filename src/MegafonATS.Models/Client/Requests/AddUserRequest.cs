﻿using MegafonATS.Models.Client.Responses;
using MegafonATS.Models.Enums;
using System.Text.Json.Serialization;

namespace MegafonATS.Models.Client.Requests
{
    public class AddUserRequest : IRequestModel
    {
        public string Login { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Position { get; set; }
        public string Email { get; set; }
        [JsonPropertyName("ext")]

        public string UserExt { get; set; }
        public UserRole Role { get; set; }
        [JsonPropertyName("mobile")]
        public string UserPhone { get; set; }
        public MobileRedirect MobileRedirect { get; set; }
    }
}

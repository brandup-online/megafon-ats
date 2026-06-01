using System.Text.Json.Serialization;

namespace MegafonATS.Client.Models.Responses.Groups
{
    public class GroupResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        [JsonPropertyName("ext")]
        public string UserExt { get; set; }
        public CallOrder? CallOrder { get; set; }
        public int CallDuration { get; set; }
        public List<GroupMember> Users { get; set; }
        public TimeoutModel Timeout { get; set; }
        public Advaced Advanced { get; set; }
        public bool QueuePosition { get; set; }
    }

    public class TimeoutModel
    {
        public int Time { get; set; }
        public string Target { get; set; }
        public string User { get; set; }
        public string Group { get; set; }
        public string Telnum { get; set; }
    }

    public class GroupMember
    {
        public string Login { get; set; }
        public bool CallsEnable { get; set; }
        public bool GroupHead { get; set; }
        public bool IsAbsenceActive { get; set; }
        public bool IsForwardingDisabled { get; set; }
    }
}

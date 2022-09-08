namespace MegafonATS.Models.Client.Responses
{
    public class GroupResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string UserExt { get; set; }
        public string CallOrder { get; set; }
        public string CallDuration { get; set; }
        public List<GroupMember> Users { get; set; }
        public TimeoutModel Timeout { get; set; }
    }

    public class TimeoutModel
    {
        public int Time { get; set; }
        public string Target { get; set; }
        public string User { get; set; }
    }

    public class GroupMember
    {
        public string Login { get; set; }
        public bool CallsEnable { get; set; }
    }
}

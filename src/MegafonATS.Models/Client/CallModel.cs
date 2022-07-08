namespace MegafonATS.Models.Client
{
    public class CallModel
    {
        public string UID { get; set; }
        public CallDirection Type { get; set; }
        public string Client { get; set; }
        public string Account { get; set; }
        public string Via { get; set; }
        public string Start { get; set; }
        public string Wait { get; set; }
        public string Duration { get; set; }
        public Uri Record { get; set; }
    }
}
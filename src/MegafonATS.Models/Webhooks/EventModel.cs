namespace MegafonATS.Models.Webhooks
{
    public class EventModel
    {
        public string Type { get; set; }
        public string Phone { get; set; }
        public string Diversion { get; set; }
        public string User { get; set; }
        public string GroupRealName { get; set; }
        public string Ext { get; set; }
        public string Telnum { get; set; }
        public string Direction { get; set; }
        public string Callid { get; set; }

    }
}

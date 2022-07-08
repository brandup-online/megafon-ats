﻿namespace MegafonATS.Models.Webhooks
{
    public class HistoryModel
    {
        public CallDirection Type { get; set; }
        public string User { get; set; }
        public string Ext { get; set; }
        public string groupRealName { get; set; }
        public string Telnum { get; set; }
        public string Phone { get; set; }
        public string Diversion { get; set; }
        public DateTime Start { get; set; }
        public int Duration { get; set; }
        public string Callid { get; set; }
        public Uri Link { get; set; }
        public int Rating { get; set; }
        public string Crm_token { get; set; }
        public string Status { get; set; }
    }
}
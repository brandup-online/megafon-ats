namespace MegafonATS.Models.Client.Responses
{
    public class InfoResponse
    {
        public string Search { get; set; }
        public int Start { get; set; }
        public int Limit { get; set; }
        public int Total { get; set; }
        public int Next { get; set; }
    }
}
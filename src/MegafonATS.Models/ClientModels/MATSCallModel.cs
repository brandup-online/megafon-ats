namespace MefafonATS.Model.ClientModels
{
    public class MATSCallModel
    {
        public string UID { get; set; }
        public string Type { get; set; }
        public string Client { get; set; }
        public string Account { get; set; }
        public string Via { get; set; }
        public string Start { get; set; }
        public string Wait { get; set; }
        public string Duration { get; set; }
        public string Record { get; set; }
        //api не возвращает контроль качества
        //public string QualityControl { get; set; }



    }
}

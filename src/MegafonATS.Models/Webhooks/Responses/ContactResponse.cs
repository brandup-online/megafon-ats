namespace MegafonATS.Models.Webhooks.ResponseModels
{
    public class ContactResponse
    {
        /// <summary>
        /// Название контакта которое высветится 
        /// на коммуникаторе
        /// </summary>
        public string ContactName { get; set; }

        /// <summary>
        /// Имя ответсвенного сотрудника  в атс 
        /// </summary>
        public string Responsible { get; set; }
    }
}

using System.Text.Json.Serialization;

namespace MegafonATS.Models.Webhooks.ResponseModels
{
    public class ContactResponse
    {
        /// <summary>
        /// Название контакта которое высветится 
        /// на коммуникаторе
        /// </summary>
        [JsonPropertyName("contact_name")]
        public string ContactName { get; set; }
        /// <summary>
        /// Имя ответсвенного сотрудника  в атс 
        /// </summary>
        [JsonPropertyName("responsible")]
        public string Responsible { get; set; }
    }
}

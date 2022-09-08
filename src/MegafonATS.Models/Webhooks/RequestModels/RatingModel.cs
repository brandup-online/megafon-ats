using System.Text.Json.Serialization;

namespace MegafonATS.Models.Webhooks.RequestModels
{
    public class RatingModel
    {
        /// <summary>
        /// номер телефона клиента, с которого или
        /// на который произошел звонок
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// оценка качества 
        /// </summary>
        public string Rating { get; set; }

        /// <summary>
        /// идентификатор пользователя АТС
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// внутренний номер пользователя АТС, если есть
        /// </summary>
        [JsonPropertyName("ext")]
        public string UserExt { get; set; }

        /// <summary>
        /// уникальный id звонка 
        /// </summary>
        public string CallId { get; set; }
    }
}
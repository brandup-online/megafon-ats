using MegafonATS.Models.Attributes;
using MegafonATS.Models.Webhooks.Requests;
using System.ComponentModel.DataAnnotations;

namespace MegafonATS.Models.Webhooks.RequestModels
{
    public class RatingModel : IWebHookModel
    {
        /// <summary>
        /// номер телефона клиента, с которого или
        /// на который произошел звонок
        /// </summary>
        [Required]
        [MapName("callid")]
        public string CallId { get; set; }

        /// <summary>
        /// идентификатор пользователя АТС
        /// </summary>
        [Required]
        public string User { get; set; }

        /// <summary>
        /// внутренний номер пользователя АТС, если есть
        /// </summary>
        [MapName("ext")]
        public string UserExt { get; set; }

        /// <summary>
        /// идентификатор пользователя АТС
        /// </summary>
        [Required]
        public string Phone { get; set; }
        /// <summary>
        /// оценка качества 
        /// </summary>
        [Required]
        public string Rating { get; set; }
    }
}
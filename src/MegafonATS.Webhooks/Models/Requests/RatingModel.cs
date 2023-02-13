using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace MegafonATS.Webhooks.Models.Requests
{
    public class RatingModel : WebHookModel
    {
        /// <summary>
        /// номер телефона клиента, с которого или
        /// на который произошел звонок
        /// </summary>
        [Required]

        [BindProperty(Name = "callid")]
        public string CallId { get; set; }

        /// <summary>
        /// идентификатор пользователя АТС
        /// </summary>
        [Required]
        public string User { get; set; }

        /// <summary>
        /// внутренний номер пользователя АТС, если есть
        /// </summary>
        [BindProperty(Name = "ext")]
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
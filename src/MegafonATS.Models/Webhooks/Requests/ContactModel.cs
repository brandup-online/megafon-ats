using MegafonATS.Models.Webhooks.Requests;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MegafonATS.Models.Webhooks.RequestModels
{
    public class ContactModel : WebHookModel
    {
        /// <summary>
        /// Уникальный id звонка, совпадает для всех связанных звонков
        /// </summary>
        [Required]
        [BindProperty(Name = "callid")]
        public string CallId { get; set; }

        /// <summary>
        /// Номер телефона клиента 
        /// </summary>
        [Required]
        public string Phone { get; set; }

        /// <summary>
        /// Личный номер телефона, через
        /// который прошел входящий вызовS
        /// </summary>
        public string Diversion { get; set; }
    }
}
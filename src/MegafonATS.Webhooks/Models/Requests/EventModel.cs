using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace MegafonATS.Webhooks.Models.Requests
{
    public class EventModel : WebHookModel
    {
        [Required]
        [BindProperty(Name = "callid")]
        public string CallId { get; set; }

        [Required]
        public string User { get; set; }

        public string GroupRealName { get; set; }

        [BindProperty(Name = "ext")]
        public string UserExt { get; set; }

        [BindProperty(Name = "telnum")]
        public string UserPhone { get; set; }

        [BindProperty(Name = "telnum_name")]
        public string TelnumName { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public WebhookCallDirection? Direction { get; set; }

        [Required]
        public EventType? Type { get; set; }

        public string Diversion { get; set; }

        /// <summary>
        /// Уникальный id переведенного звонка (при событии TRANSFERRED)
        /// </summary>
        [BindProperty(Name = "second_callid")]
        public string SecondCallId { get; set; }
    }

    public enum EventType
    {
        /// <summary>
        /// Поступил входящий звонок
        /// </summary>
        Incoming,

        /// <summary>
        /// Звонок успешно принят
        /// </summary>
        Accepted,

        /// <summary>
        /// Звонок успешно завершен
        /// </summary>
        Completed,

        /// <summary>
        /// Звонок сброшен
        /// </summary>
        Cancelled,

        /// <summary>
        /// Менеджер совершает исходящий звонок
        /// </summary>
        Outgoing,

        /// <summary>
        /// Входящий звонок переведен на другого сотрудника
        /// </summary>
        Transferred
    }
}

using MegafonATS.Models.Attributes;
using MegafonATS.Models.Webhooks.Enums;
using MegafonATS.Models.Webhooks.Requests;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MegafonATS.Models.Webhooks.RequestModels
{
    public class EventModel : WebHookModel
    {
        /// <summary>
        /// уникальный id звонка, совпадает для
        /// всех связанных звонков
        /// </summary>
        [Required]
        [MapName("callid")]
        [BindProperty(Name = "callid")]
        public string CallId { get; set; }

        /// <summary>
        /// идентификатор пользователя АТС (необходим для сопоставления на стороне CRM)
        /// </summary>
        [Required]
        public string User { get; set; }

        /// <summary>
        /// название отдела, если входящий звонок
        /// прошел через отдел
        /// </summary>
        public string GroupRealName { get; set; }

        /// <summary>
        /// внутренний номер пользователя АТС,
        /// если есть
        /// </summary>
        [MapName("ext")]
        [BindProperty(Name = "ext")]
        public string UserExt { get; set; }

        /// <summary>
        /// прямой телефонный номер пользователя
        /// АТС, если есть
        /// </summary>
        [MapName("telnum")]
        [BindProperty(Name = "telnum")]
        public string UserPhone { get; set; }

        /// <summary>
        /// номер телефона клиента
        /// </summary>
        [Required]
        public string Phone { get; set; }

        /// <summary>
        /// тип звонка in/out (входящий/исходящий)
        /// </summary>
        [Required]
        public WebhookCallDirection? Direction { get; set; }
        /// <summary>
        /// type - это тип события, связанного со звонком
        /// </summary>
        [Required]
        public EventType? Type { get; set; }

        /// <summary>
        /// ваш номер телефона, через который пришел входящий вызов
        /// </summary>
        public string Diversion { get; set; }


    }

    public enum EventType
    {
        /// <summary>
        /// Поступил входящий звонок (у менеджера должен начать звонить телефон в это время)
        /// </summary>
        Incoming,

        /// <summary>
        /// Звонок успешно принят (менеджер снял трубку). В этот момент
        /// можно убрать всплывающую карточку контакта в CRM
        /// </summary>
        Accepted,

        /// <summary>
        /// Звонок успешно завершен(менеджер или клиент положили трубку после разговора)
        /// </summary>
        Completed,

        /// <summary>
        /// Звонок сброшен (клиент не  дождался пока менеджер снимет трубку,
        /// либо если это был звонок на группу менеджеров, на звонок мог ответить ктото еще)
        /// </summary>
        Cancelled,

        /// <summary>
        /// Менеджер совершает исходящий звонок (ВАТС пытается дозвониться до клиента)
        /// </summary>
        Outgoing,

        /// <summary>
        /// Входящий звонок переведен на другого сотрудника
        /// </summary>
        Transferred
    }
}
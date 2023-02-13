using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace MegafonATS.Webhooks.Models.Requests
{
    public class HistoryModel : WebHookModel
    {
        /// <summary>
        /// уникальный id звонка
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
        /// название отдела, если входящий звонок прошел через отдел
        /// </summary>
        public string GroupRealName { get; set; }

        /// <summary>
        /// прямой телефонный номер пользователя АТС, если есть
        /// </summary>
        [BindProperty(Name = "telnum")]
        public string UserPhone { get; set; }

        /// <summary>
        /// номер телефона клиента, с которого или на который произошел звонок
        /// </summary> 
        [Required]
        public string Phone { get; set; }

        /// <summary>
        /// тип звонка входящий/исходящий
        /// </summary>
        [Required]
        [BindProperty(Name = "type")]
        public WebhookCallDirection? Type { get; set; }

        /// <summary>
        /// статус входящего/исходящего звонка
        /// </summary>
        [Required]
        public WebhookCallStatus? Status { get; set; }

        /// <summary>
        /// ваш номер телефона, через который пришел входящий вызов
        /// </summary>
        [Required]
        public string Diversion { get; set; }

        /// <summary>
        /// время начала звонка в формате YYYYmmddTHHMMSSZ
        /// </summary>
        [Required]
        public DateTime Start { get; set; }

        /// <summary>
        /// общая длительность звонка в секундах 
        /// </summary>
        [Required]
        public int Duration { get; set; }

        /// <summary>
        /// ссылка на запись звонка, если она включена в Виртуальной АТС
        /// </summary>
        public Uri Link { get; set; }

        /// <summary>
        /// оценка качества звонка
        /// </summary>
        public int Rating { get; set; }
    }
}
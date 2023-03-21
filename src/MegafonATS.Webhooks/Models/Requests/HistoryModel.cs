using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MegafonATS.Webhooks.Models.Requests
{
    public class HistoryModel : WebHookModel
    {
        /// <summary>
        /// Уникальный id звонка
        /// </summary>
        [Required]
        [BindProperty(Name = "callid")]
        public string CallId { get; set; }

        /// <summary>
        /// Идентификатор пользователя АТС 
        /// </summary>
        [Required]
        public string User { get; set; }

        /// <summary>
        /// Внутренний номер пользователя АТС, если есть
        /// </summary>
        [BindProperty(Name = "ext")]
        public string UserExt { get; set; }

        /// <summary>
        /// Название отдела, если входящий звонок прошел через отдел
        /// </summary>
        public string GroupRealName { get; set; }

        /// <summary>
        /// Прямой телефонный номер пользователя АТС, если есть
        /// </summary>
        [BindProperty(Name = "telnum")]
        public string UserPhone { get; set; }

        /// <summary>
        /// Номер телефона клиента, с которого или на который произошел звонок
        /// </summary> 
        [Required]
        public string Phone { get; set; }

        /// <summary>
        /// Тип звонка входящий/исходящий
        /// </summary>
        [Required]
        [BindProperty(Name = "type")]
        public WebhookCallDirection? Type { get; set; }

        /// <summary>
        /// Статус входящего/исходящего звонка
        /// </summary>
        [Required]
        public WebhookCallStatus? Status { get; set; }

        /// <summary>
        /// Ваш номер телефона, через который пришел входящий вызов
        /// </summary>
        [Required]
        public string Diversion { get; set; }

        /// <summary>
        /// Время начала звонка в формате YYYYmmddTHHMMSSZ
        /// </summary>
        [Required]
        public DateTime Start { get; set; }

        /// <summary>
        /// Общая длительность звонка в секундах 
        /// </summary>
        [Required]
        public int? Duration { get; set; }

        /// <summary>
        /// Ссылка на запись звонка, если она включена в Виртуальной АТС
        /// </summary>
        public Uri Link { get; set; }

        /// <summary>
        /// Оценка качества звонка
        /// </summary>
        public int Rating { get; set; }
    }
}
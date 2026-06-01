using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MegafonATS.Webhooks.Models.Requests
{
    public class HistoryModel : WebHookModel
    {
        [Required]
        [BindProperty(Name = "callid")]
        public string CallId { get; set; }

        [Required]
        public string User { get; set; }

        [BindProperty(Name = "ext")]
        public string UserExt { get; set; }

        /// <summary>
        /// Идентификатор отдела, если входящий звонок прошел через отдел
        /// </summary>
        public string Group { get; set; }

        public string GroupRealName { get; set; }

        [BindProperty(Name = "telnum")]
        public string UserPhone { get; set; }

        [BindProperty(Name = "telnum_name")]
        public string TelnumName { get; set; }

        [Required]
        public string Phone { get; set; }

        /// <summary>
        /// Тип звонка in/out
        /// </summary>
        [Required]
        [BindProperty(Name = "type")]
        public WebhookCallDirection? Type { get; set; }

        [Required]
        public WebhookCallStatus? Status { get; set; }

        [Required]
        public string Diversion { get; set; }

        /// <summary>
        /// Время начала звонка в формате YYYYmmddTHHMMSSZ
        /// </summary>
        [Required]
        public DateTime Start { get; set; }

        /// <summary>
        /// Время ожидания ответа (сек)
        /// </summary>
        [Required]
        public int? Wait { get; set; }

        /// <summary>
        /// Общая длительность звонка в секундах
        /// </summary>
        [Required]
        public int? Duration { get; set; }

        public Uri Link { get; set; }

        public int? Rating { get; set; }

        /// <summary>
        /// Статус пропущенного звонка: 1-клиент перезвонил, 2-перезвонили, 3-не перезванивали, 4-не дозвонились
        /// </summary>
        [BindProperty(Name = "missedStatus")]
        public string MissedStatus { get; set; }
    }
}

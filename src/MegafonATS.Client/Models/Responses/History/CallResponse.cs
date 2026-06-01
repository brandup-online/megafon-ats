using System.Text.Json.Serialization;

namespace MegafonATS.Client.Models.Responses.History
{
    public class CallResponse
    {
        /// <summary>
        /// Уникальный идентификатор звонка
        /// </summary>
        [JsonPropertyName("uid")]
        public string CallId { get; set; }

        /// <summary>
        /// Тип звонка (входящий/исходящий)
        /// </summary>
        [JsonPropertyName("type")]
        public ClientCallDirection Direction { get; set; }

        /// <summary>
        /// Статус звонка (успешный/пропущенный/не состоялся)
        /// </summary>
        public ClientCallStatus Status { get; set; }

        /// <summary>
        /// Номер клиента
        /// </summary>
        public string Client { get; set; }

        /// <summary>
        /// Номер телефона, через который пришел звонок
        /// </summary>
        public string Diversion { get; set; }

        /// <summary>
        /// Имя номера, через который пришел звонок
        /// </summary>
        [JsonPropertyName("telnum_name")]
        public string TelnumName { get; set; }

        /// <summary>
        /// Адресат входящего звонка
        /// </summary>
        public string Destination { get; set; }

        /// <summary>
        /// Логин сотрудника
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// Имя сотрудника
        /// </summary>
        [JsonPropertyName("user_name")]
        public string UserName { get; set; }

        /// <summary>
        /// Имя отдела через который прошел звонок
        /// </summary>
        [JsonPropertyName("group_name")]
        public string GroupName { get; set; }

        /// <summary>
        /// Время начала звонка
        /// </summary>
        public DateTime Start { get; set; }

        /// <summary>
        /// Время ожидания на линии (сек)
        /// </summary>
        public int Wait { get; set; }

        /// <summary>
        /// Длительность разговора (сек)
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        /// Ссылка на запись разговора
        /// </summary>
        public Uri Record { get; set; }

        /// <summary>
        /// Оценка качества обслуживания
        /// </summary>
        public int? Rating { get; set; }

        /// <summary>
        /// Примечание
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// Статус пропущенного звонка: 1-клиент перезвонил, 2-перезвонили, 3-не перезванивали, 4-не дозвонились
        /// </summary>
        [JsonPropertyName("missed_status")]
        public int? MissedStatus { get; set; }
    }
}

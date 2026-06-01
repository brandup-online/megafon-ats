using System.Text.Json.Serialization;

namespace MegafonATS.Client.Models.Requests
{
    public class HistoryRequest : IRequestModel
    {
        /// <summary>
        /// Фильтр по уникальному идентификатору звонка
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Uid { get; set; }

        /// <summary>
        /// Начало периода для выгрузки данных YYYYmmddTHHMMSSZ
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateTime? Start { get; set; }

        /// <summary>
        /// Окончание периода для выгрузки данных YYYYmmddTHHMMSSZ
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateTime? End { get; set; }

        /// <summary>
        /// Период, за который необходимо выгрузить данные
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Period? Period { get; set; }

        /// <summary>
        /// Тип звонка
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public FilterCallType? Type { get; set; }

        /// <summary>
        /// Лимит записей в полученном результате
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? Limit { get; set; }

        /// <summary>
        /// Фильтр по логину сотрудника
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string User { get; set; }

        /// <summary>
        /// Фильтр по номеру ВАТС
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Diversion { get; set; }

        /// <summary>
        /// Фильтр по номеру клиента
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Client { get; set; }

        /// <summary>
        /// Отображать первого ответившего сотрудника
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("first_answered")]
        public bool? FirstAnswered { get; set; }

        /// <summary>
        /// Отображать статусы пропущенных звонков
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("process_missed")]
        public bool? ProcessMissed { get; set; }
    }
}

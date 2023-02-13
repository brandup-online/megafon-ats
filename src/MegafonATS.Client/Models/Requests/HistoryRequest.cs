using System.Text.Json.Serialization;

namespace MegafonATS.Client.Models.Requests
{
    public class HistoryRequest : IRequestModel
    {
        /// <summary>
        /// Начало периода для выгрузки данных YYYYmmddTHHMMSSZ
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateTime? Start { get; set; }
        /// <summary>
        /// Окончание периода для выгрузки данных в формате YYYYmmddTHHMMSSZ
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateTime? End { get; set; }
        /// <summary>
        /// Период, за который необходимо
        /// выгрузить данные
        /// </summary>

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public FilterPeriod? Period { get; set; }
        /// <summary>
        /// Тип звонка
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public FilterCallType? Type { get; set; }
        /// <summary>
        /// Лимит записей в полученном результате
        /// </summary>
        public int Limit { get; set; } = 100;
    }
}

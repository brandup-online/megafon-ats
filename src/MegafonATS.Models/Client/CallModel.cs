namespace MegafonATS.Models.Client
{
    public class CallModel
    {
        /// <summary>
        /// Уникальный идентификатор звонка
        /// </summary>
        public string CallId { get; set; }
        /// <summary>
        /// Тип вызова: in / out / missed
        /// </summary>
        public CallDirection Type { get; set; }
        /// <summary>
        /// Номер клиента
        /// </summary>
        public string Client { get; set; }
        /// <summary>
        /// Логин сотрудника, который разговаривал с клиентом или имя группы или код:
        /// ivr / fax, если звонок не дошел до сотрудника
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// Номер телефона, через который пришел входящий звонок или АОН для    
        /// исходящего вызова
        /// </summary>
        public string Via { get; set; }
        /// <summary>
        /// Время начала звонка в UTC
        /// </summary>
        public string Start { get; set; }
        /// <summary>
        /// Время ожидания на линии (секунд)
        /// </summary>
        public string Wait { get; set; }
        /// <summary>
        /// Длительность разговора (секунд)
        /// </summary>
        public string Duration { get; set; }
        /// <summary>
        /// Ссылка на запись разговора
        /// </summary>
        public Uri Record { get; set; }
        /// <summary>
        /// Оценка качества разговора, если есть
        /// </summary>
        public string QualityControl { get; set; }
    }
}
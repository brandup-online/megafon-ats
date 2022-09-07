namespace MegafonATS.Models.Webhooks.RequestModels
{
    public class HistoryModel
    {
        /// <summary>
        /// тип звонка входящий/исходящий
        /// </summary>
        public CallDirection Type { get; set; }

        /// <summary>
        /// идентификатор пользователя АТС 
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// внутренний номер пользователя АТС, если есть
        /// </summary>
        public string Ext { get; set; }

        /// <summary>
        /// название отдела, если входящий звонок прошел через отдел
        /// </summary>
        public string GroupRealName { get; set; }

        /// <summary>
        /// прямой телефонный номер пользователя АТС, если есть
        /// </summary>
        public string Telnum { get; set; }

        /// <summary>
        /// номер телефона клиента, с которого или на который произошел звонок
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// ваш номер телефона, через который пришел входящий вызов
        /// </summary>
        public string Diversion { get; set; }

        /// <summary>
        /// время начала звонка в формате YYYYmmddTHHMMSSZ
        /// </summary>
        public DateTime Start { get; set; }

        /// <summary>
        /// общая длительность звонка в секундах 
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        /// уникальный id звонка
        /// </summary>
        public string CallId { get; set; }

        /// <summary>
        /// ссылка на запись звонка, если она включена в Виртуальной АТС
        /// </summary>
        public Uri Link { get; set; }

        /// <summary>
        /// оценка качества звонка
        /// </summary>
        public int Rating { get; set; }

        /// <summary>
        /// ключ (token) от CRM, установленный в веб-кабинете
        /// </summary>
        public string Crm_token { get; set; }

        /// <summary>
        /// статус входящего/исходящего звонка
        /// </summary>
        public CallStatus Status { get; set; }
    }
}
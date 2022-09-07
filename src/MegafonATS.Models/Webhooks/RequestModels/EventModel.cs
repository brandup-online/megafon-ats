namespace MegafonATS.Models.Webhooks.RequestModels
{
    public class EventModel
    {
        /// <summary>
        /// type - это тип события, связанного со звонком
        /// </summary>
        public EventType Type { get; set; }

        /// <summary>
        /// номер телефона клиента
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// ваш номер телефона, через который пришел входящий вызов
        /// </summary>
        public string Diversion { get; set; }

        /// <summary>
        /// идентификатор пользователя АТС (необходим для сопоставления на стороне CRM)
        /// </summary>
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
        public string Ext { get; set; }

        /// <summary>
        /// прямой телефонный номер пользователя
        /// АТС, если есть
        /// </summary>
        public string Telnum { get; set; }

        /// <summary>
        /// тип звонка in/out (входящий/исходящий)
        /// </summary>
        public CallDirection Direction { get; set; }

        /// <summary>
        /// уникальный id звонка, совпадает для
        /// всех связанных звонков
        /// </summary>
        public string CallId { get; set; }
    }

    public enum EventType
    {
        /// <summary>
        /// Поступил входящий звонок (у
        /// менеджера должен начать звонить телефон в это время)
        /// </summary>
        INCOMING,

        /// <summary>
        /// Звонок успешно принят (менеджер снял трубку). В этот момент
        /// можно убрать всплывающую карточку контакта в CRM
        /// </summary>
        ACCEPTED,

        /// <summary>
        /// Звонок успешно завершен(менеджер или клиент положили трубку после разговора)
        /// </summary>
        COMPLETED,

        /// <summary>
        /// Звонок сброшен (клиент не  дождался пока менеджер снимет трубку,
        /// либо если это был звонок на группу менеджеров, на звонок мог ответить ктото еще)
        /// </summary>
        CANCELLED,

        /// <summary>
        /// Менеджер совершает исходящий звонок (ВАТС пытается дозвониться до клиента)
        /// </summary>
        OUTGOING,

        /// <summary>
        /// Входящий звонок переведен на другого сотрудника
        /// </summary>
        TRANSFERRED
    }
}
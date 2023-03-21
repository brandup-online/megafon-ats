namespace MegafonATS.Webhooks.Models
{
    public enum WebhookCallDirection
    {
        In,
        Out,
    }

    public enum WebhookCallStatus
    {
        /// <summary>
        /// Успешный входящий (исходящий) звонок
        /// </summary>
        Success,
        /// <summary>
        ///  Пропущенный входящий (исходящий) звонок
        /// </summary>
        Missed,
        /// <summary>
        /// Входящий (исходящий) звонок отменен;
        /// </summary>
        Cancel,
        /// <summary>
        ///  Получен ответ «Занято» (только исходящий)
        /// </summary>
        Busy,
        /// <summary>
        /// Получен ответ «Абонент недоступен» (только исходящий)
        /// </summary>
        NotAvailable,
        /// <summary>
        ///  Получен ответ «Звонки на это направление запрещены» (только исходящий);
        /// </summary>
        NotAllowed,
        /// <summary>
        /// Получен ответ «Вызываемый абонент не найден, нет такого SIP номера» (только исходящий).
        /// </summary>
        NotFound
    }
}
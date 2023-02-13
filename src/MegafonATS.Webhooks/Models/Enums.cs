namespace MegafonATS.Webhooks.Models
{
    public enum WebhookCallDirection
    {
        In,
        Out,
        Missed
    }

    public enum WebhookCallStatus
    {
        Success, // Успешный входящий (исходящий) звонок
        Missed, // Пропущенный входящий (исходящий) звонок
        Cancel, // Входящий (исходящий) звонок отменен;
        Busy, // Получен ответ «Занято» (только исходящий)
        NotAvailable, // Получен ответ «Абонент недоступен» (только исходящий)
        NotAllowed, // Получен ответ «Звонки на это направление запрещены» (только исходящий);
        NotFound // Получен ответ «Вызываемый абонент не найден, нет такого SIP номера» (только исходящий).
    }
}
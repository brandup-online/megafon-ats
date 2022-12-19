using MegafonATS.Models.Webhooks.RequestModels;
using MegafonATS.Models.Webhooks.ResponseModels;

namespace MegafonATS.Webhooks
{
    public interface IMegafonAtsEvents
    {
        /// <summary>
        /// После успешного звонка в CRM отправляется запрос с данными о звонке и ссылкой на запись разговора. Команда может быть использована для сохранения в данных ваших клиентов истории и записей входящих и исходящих звонков.
        /// </summary>
        /// <param name="history">Параметры команды</param>
        /// <param name="cancellationToken">Токен отмены</param>
        Task HistoryAsync(string token, HistoryModel history, CancellationToken cancellationToken = default);

        /// <summary>
        /// С помощью команды event АТС отправляет в CRM уведомления о событиях звонков пользователям: появлении, принятии или завершении звонка.Команда может быть использована для отображения всплывающей карточки клиента в интерфейсе CRM.
        /// </summary>
        /// <param name="_event">Параметры команды</param>
        /// <param name="cancellationToken">Токен отмены</param>
        Task EventAsync(string token, EventModel @event, CancellationToken cancellationToken = default);

        /// <summary>
        /// С помощью команды contact CRM получает информацию о названии клиента и ответственном за него сотруднике по номеру его телефона.Команда вызывается при поступлении нового входящего звонка. Команда contact используется для отображения на экране IP‑телефона или в коммуникаторе на ПК сотрудника названия клиента.
        /// </summary>
        /// <param name="contact">Параметры команды</param>
        /// <param name="cancellationToken">Токен отмены</param>
        Task<ContactResponse> ContactAsync(string token, ContactModel contact, CancellationToken cancellationToken = default);

        /// <summary>
        /// С помощью команды rating в CRM отправляется запрос с оценкой, которую клиент поставил сотруднику после разговора
        /// </summary>
        /// <param name="rating">Параметры команды</param>
        /// <param name="cancellationToken">Токен отмены</param>
        Task RatingAsync(string token, RatingModel rating, CancellationToken cancellationToken = default);
    }
}
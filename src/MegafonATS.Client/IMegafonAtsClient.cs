using MegafonATS.Models.Client;

namespace MegafonATS.Client
{
    public interface IMegafonAtsClient
    {
        /// <summary>
        /// Запрос от CRM к Виртуальной АТС для получения сотрудников.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>Коллекцию отделов</returns>
        Task<ClientResult<IEnumerable<AccountModel>>> AccountsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Запрос от CRM к Виртуальной АТС для получения отделов для конкретного сотрудника
        /// </summary>
        /// <param name="user">идентификатор сотрудника Виртуальной АТС</param>
        /// <param name="cancellationToken">Коллекцию отделов</param>
        /// <returns>Коллекцию отделов</returns>
        Task<ClientResult<IEnumerable<GroupModel>>> GroupsAsync(string user, CancellationToken cancellationToken = default);

        /// <summary>
        /// Запрос от CRM к Виртуальной АТС для получения отделов
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>Коллекцию отделов</returns>
        Task<ClientResult<IEnumerable<GroupModel>>> GroupsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Команда необходимая для того, чтобы инициировать звонок от менеджера клиенту. В результате
        /// успешного выполнения команды, Виртуальная АТС сделает сначала звонок на телефон менеджера,
        /// а потом соединит его с клиентом.Команда может использоваться, например, для звонка по клику
        /// на номер клиента в вашей CRM или базе данных.
        /// </summary>
        /// <param name="user">идентификатор сотрудника Виртуальной АТС</param>
        /// <param name="phoneNumber">"номер телефона клиента"</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Уникальный идентификатор звонка</returns>
        Task<ClientResult<CurrentCallModel>> MakeCallAsync(string user, string phoneNumber, CancellationToken cancellationToken = default);

        /// <summary>
        /// Команда необходима для того, чтобы получить из Виртуальной АТС историю звонков за нужный период времени.
        /// </summary>
        /// <param name="period">период времени за который нужно выгрузить данные.</param>
        /// <param name="type">тип звонка</param>
        /// <param name="limit">результат должен содержать не более, чем limit записей </param>
        /// <param name="cancellationToken"></param>
        /// <returns>Коллекцию объектов с информацией о звонках</returns>
        Task<ClientResult<IEnumerable<CallModel>>> HistoryAsync(FilterPeriod period, FilterCallType type, int limit = int.MaxValue, CancellationToken cancellationToken = default);

        /// <summary>
        /// Команда необходима для того, чтобы получить из Виртуальной АТС историю звонков за нужный период времени.
        /// </summary>
        /// <param name="start">начало периода для выгрузки данных</param>
        /// <param name="end">окончание периода для выгрузки данных</param>
        /// <param name="type">тип звонка</param>
        /// <param name="limit">результат должен содержать не более, чем limit записей</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Коллекцию объектов с информацией о звонках</returns>
        Task<ClientResult<IEnumerable<CallModel>>> HistoryAsync(DateTime start, DateTime end, FilterCallType type, int limit = int.MaxValue, CancellationToken cancellationToken = default);

        /// <summary>
        /// Запрос от CRM к Виртуальной АТС для включения / выключения приема звонков сотрудником в конкретном отделе.
        /// </summary>
        /// <param name="user">идентификатор сотрудника Виртуальной АТС</param>
        /// <param name="groupId">идентификатор отдела АТС, в которомнадо выключить/включить прием звонков </param>
        /// <param name="status">on - чтобы включить прием звонков, off - чтобы выключить прием звонков</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Информация о успехе/неуспехе операции</returns>
        Task<ClientResult> SubscribeOnCallsAsync(string user, string groupId, SubscriptionStatus status, CancellationToken cancellationToken = default);
        /// <summary>
        /// Запрос от CRM к Виртуальной АТС для включения / выключения приема звонков сотрудником во всех его отделах
        /// </summary>
        /// <param name="user">идентификатор сотрудника Виртуальной АТС</param>
        /// <param name="groupId">идентификатор отдела АТС, в которомнадо выключить/включить прием звонков </param>
        /// <param name="status">on - чтобы включить прием звонков, off - чтобы выключить прием звонков</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Информация о успехе/неуспехе операции</returns>
        Task<ClientResult> SubscribeOnCallsAsync(string user, SubscriptionStatus status, CancellationToken cancellationToken = default);

        /// <summary>
        /// Запрос от CRM к Виртуальной АТС для проверки факта приема звонков сотрудником в конкретном отделе
        /// </summary>
        /// <param name="user">идентификатор сотрудника Виртуальной АТС</param>
        /// <param name="groupId">идентификатор отдела АТС, для которого надо выполнить проверку</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ClientResult<StatusModel>> SubscriptionStatusAsync(string user, string groupId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Запрос от CRM к Виртуальной АТС позволяет включить или выключить прием звонков сотрудником Виртуальной АТС.
        /// </summary>
        /// <param name="user">идентификатор сотрудника Виртуальной АТС </param>
        /// <param name="state">true - выключен,  false - включен </param>
        /// <param name="cancellationToken"></param>
        /// <returns>Информация об успехе/неуспехе операции</returns>

        Task<ClientResult> SetDnDAsync(string user, bool state, CancellationToken cancellationToken = default);

        /// <summary>
        /// Запрос от CRM к Виртуальной АТС позволяет узнать включен или выключен прием звонков сотрудником Виртуальной АТС.
        /// </summary>
        /// <param name="user">идентификатор сотрудника Виртуальной АТС</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ClientResult<DnDModel>> GetDnDAsync(string user, CancellationToken cancellationToken = default);
    }
}
using MegafonATS.Models.Client;

namespace MegafonATS.Client
{
    public interface IMegafonAtsClient
    {
        Task<ClientResult<IEnumerable<UserModel>>> AccountsAsync(CancellationToken cancellationToken = default);
        Task<ClientResult<IEnumerable<GroupModel>>> GroupsAsync(string user, CancellationToken cancellationToken = default);
        Task<ClientResult<IEnumerable<GroupModel>>> GroupsAsync(CancellationToken cancellationToken = default);
        Task<ClientResult<CurrentCallModel>> MakeCallAsync(string user, string phoneNumber, CancellationToken cancellationToken = default);
        Task<ClientResult<IEnumerable<CallModel>>> HistoryAsync(EPeriod period, ECallType type, int limit = int.MaxValue, CancellationToken cancellationToken = default);
        Task<ClientResult<IEnumerable<CallModel>>> HistoryAsync(DateTime start, DateTime end, ECallType type, int limit = int.MaxValue, CancellationToken cancellationToken = default);
        Task<ClientResult> SubscribeOnCallsAsync(string user, string group_id, ESubscriptionStatus status, CancellationToken cancellationToken = default);
        Task<ClientResult<StatusModel>> SubscriptionStatusAsync(string user, string group_id, CancellationToken cancellationToken = default);
        Task<ClientResult> SetDnDAsync(string user, bool state, CancellationToken cancellationToken = default);
        Task<ClientResult<DnDModel>> GetDnDAsync(string user, CancellationToken cancellationToken = default);
    }
}
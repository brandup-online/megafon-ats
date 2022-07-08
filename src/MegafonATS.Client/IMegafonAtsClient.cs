using MegafonATS.Models.Client;

namespace MegafonATS.Client
{
    public interface IMegafonAtsClient
    {
        Task<ClientResult<IEnumerable<UserModel>>> AccountsAsync(CancellationToken cancellationToken = default);
        Task<ClientResult<IEnumerable<GroupModel>>> GroupsAsync(string user, CancellationToken cancellationToken = default);
        Task<ClientResult<IEnumerable<GroupModel>>> GroupsAsync(CancellationToken cancellationToken = default);
        Task<ClientResult<CurrentCallModel>> MakeCallAsync(string user, string phoneNumber, CancellationToken cancellationToken = default);
        Task<ClientResult<IEnumerable<CallModel>>> HistoryAsync(FilterPeriod period, FilterCallType type, int limit = int.MaxValue, CancellationToken cancellationToken = default);
        Task<ClientResult<IEnumerable<CallModel>>> HistoryAsync(DateTime start, DateTime end, FilterCallType type, int limit = int.MaxValue, CancellationToken cancellationToken = default);
        Task<ClientResult> SubscribeOnCallsAsync(string user, string groupId, SubscriptionStatus status, CancellationToken cancellationToken = default);
        Task<ClientResult<StatusModel>> SubscriptionStatusAsync(string user, string groupId, CancellationToken cancellationToken = default);
        Task<ClientResult> SetDnDAsync(string user, bool state, CancellationToken cancellationToken = default);
        Task<ClientResult<DnDModel>> GetDnDAsync(string user, CancellationToken cancellationToken = default);
    }
}
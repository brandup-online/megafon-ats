using MefafonATS.Model.ClientModels;

namespace MefafonATS.Model
{
    public interface IMegafonAtsClient
    {
        public Task<ClientResult<IEnumerable<MATSUserModel>>> AccountsAsync(CancellationToken cancellationToken = default);
        public Task<ClientResult<IEnumerable<MATSGroupModel>>> GroupsAsync(string user, CancellationToken cancellationToken = default);
        public Task<ClientResult<IEnumerable<MATSGroupModel>>> GroupsAsync(CancellationToken cancellationToken = default);
        public Task<ClientResult<MATSCurrentCallModel>> MakeCallAsync(string user, string phoneNumber, CancellationToken cancellationToken = default);
        public Task<ClientResult<IEnumerable<MATSCallModel>>> HistoryAsync(EPeriod period, ECallType type, int limit = int.MaxValue, CancellationToken cancellationToken = default);
        public Task<ClientResult<IEnumerable<MATSCallModel>>> HistoryAsync(DateTime start, DateTime end, ECallType type, int limit = int.MaxValue, CancellationToken cancellationToken = default);
        public Task<ClientResult> SubscribeOnCallsAsync(string user, string group_id, ESubscriptionStatus status, CancellationToken cancellationToken = default);
        public Task<ClientResult<StatusModel>> SubscriptionStatusAsync(string user, string group_id, CancellationToken cancellationToken = default);
        public Task<ClientResult> SetDnDAsync(string user, bool state, CancellationToken cancellationToken = default);
        public Task<ClientResult<MATSDnDModel>> GetDnDAsync(string user, CancellationToken cancellationToken = default);
    }
}
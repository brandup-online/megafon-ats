using MegafonATS.Models.Client.Responses;

namespace MegafonATS.Client.Core
{
    public class SubscriptionClient : ClientBase, ISubscriptionClient
    {
        const string apiEndpont = "/users";
        public SubscriptionClient(HttpClient httpClient, MegafonAtsOptions options) : base(httpClient, options)
        {
        }

        public async Task SubscribeUserOnGroupAsync(string login, string groupId, CancellationToken cancellationToken)
        {
            var endpoint = apiEndpont + "/" + login + "/subscription" + "?" + $"group_id={groupId}";
            var result = await ExecuteAsync(HttpMethod.Post, endpoint, null, cancellationToken);

            if (!result.IsSuccess)
                throw new Exception(result.Error);

        }
        public async Task UnsubscribeUserOnGroupAsync(string login, string groupId, CancellationToken cancellationToken)
        {
            var endpoint = apiEndpont + "/" + login + "/subscription" + "?" + $"group_id={groupId}";
            var result = await ExecuteAsync(HttpMethod.Delete, endpoint, null, cancellationToken);

            if (!result.IsSuccess)
                throw new Exception(result.Error);

        }

        public async Task<bool> GetUserSubscriptionAsync(string login, string groupId, CancellationToken cancellationToken)
        {
            var endpoint = apiEndpont + "/" + login + "/subscription" + "?" + $"group_id={groupId}";
            var result = await ExecuteGetAsync<StateResponse>(endpoint, cancellationToken);

            if (!result.IsSuccess)
                throw new Exception(result.Error);
            return result.Result.State;
        }

        public async Task SubscribeUserOnAllGroupAsync(string login, CancellationToken cancellationToken)
        {
            var endpoint = apiEndpont + "/" + login + "/subscription";
            var result = await ExecuteAsync(HttpMethod.Post, endpoint, null, cancellationToken);

            if (!result.IsSuccess)
                throw new Exception(result.Error);
        }

        public async Task UnsubscribeUserOnAllGroupAsync(string login, CancellationToken cancellationToken)
        {
            var endpoint = apiEndpont + "/" + login + "/subscription";
            var result = await ExecuteAsync(HttpMethod.Delete, endpoint, null, cancellationToken);

            if (!result.IsSuccess)
                throw new Exception(result.Error);
        }

        public async Task DoNotDisturbUserAsync(string login, CancellationToken cancellationToken)
        {
            var endpoint = apiEndpont + "/" + login + "/dnd";
            var result = await ExecuteAsync(HttpMethod.Post, endpoint, null, cancellationToken);

            if (!result.IsSuccess)
                throw new Exception(result.Error);
        }

        public async Task RevokeDoNotDisturbUserAsync(string login, CancellationToken cancellationToken)
        {
            var endpoint = apiEndpont + "/" + login + "/dnd";
            var result = await ExecuteAsync(HttpMethod.Delete, endpoint, null, cancellationToken);

            if (!result.IsSuccess)
                throw new Exception(result.Error);
        }

        public async Task<bool> GetUserDnDStatusAsync(string login, CancellationToken cancellationToken)
        {
            var endpoint = apiEndpont + "/" + login + "/dnd";
            var result = await ExecuteAsync<StateResponse>(HttpMethod.Get, endpoint, null, cancellationToken);

            if (!result.IsSuccess)
                throw new Exception(result.Error);

            return result.Result.State;
        }

    }

    internal interface ISubscriptionClient
    {
        public Task SubscribeUserOnGroupAsync(string login, string groupId, CancellationToken cancellationToken);
        public Task UnsubscribeUserOnGroupAsync(string login, string groupId, CancellationToken cancellationToken);
        public Task<bool> GetUserSubscriptionAsync(string login, string groupId, CancellationToken cancellationToken);
        public Task SubscribeUserOnAllGroupAsync(string login, CancellationToken cancellationToken);
        public Task UnsubscribeUserOnAllGroupAsync(string login, CancellationToken cancellationToken);
        public Task DoNotDisturbUserAsync(string login, CancellationToken cancellationToken);
        public Task RevokeDoNotDisturbUserAsync(string login, CancellationToken cancellationToken);
        public Task<bool> GetUserDnDStatusAsync(string login, CancellationToken cancellationToken);
    }
}

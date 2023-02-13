using MegafonATS.Client.Core.Abstract;
using MegafonATS.Client.Models.Responses.Subscriptions;
using MegafonATS.Client.Results;
using Microsoft.Extensions.Logging;

namespace MegafonATS.Client.Core
{
    public class SubscriptionClient : ClientBase, ISubscriptionClient
    {
        const string apiEndpont = "/users";

        public SubscriptionClient(HttpClient httpClient, MegafonAtsOptions options, ILogger<ClientBase> logger) : base(httpClient, options, logger) { }

        public async Task<ClientResult> SubscribeUserOnGroupAsync(string login, string groupId, CancellationToken cancellationToken)
        {
            var endpoint = apiEndpont + "/" + login + "/subscription" + "?" + $"group_id={groupId}";

            return await ExecuteAsync(HttpMethod.Post, endpoint, null, cancellationToken);
        }

        public async Task<ClientResult> UnsubscribeUserOnGroupAsync(string login, string groupId, CancellationToken cancellationToken)
        {
            var endpoint = apiEndpont + "/" + login + "/subscription" + "?" + $"group_id={groupId}";

            return await ExecuteAsync(HttpMethod.Delete, endpoint, null, cancellationToken);
        }

        public async Task<ClientResult<StateResponse>> GetUserSubscriptionAsync(string login, string groupId, CancellationToken cancellationToken)
        {
            var endpoint = apiEndpont + "/" + login + "/subscription" + "?" + $"group_id={groupId}";
            var result = await ExecuteGetAsync<StateResponse>(endpoint, cancellationToken);

            return result;
        }

        public async Task<ClientResult> SubscribeUserOnAllGroupAsync(string login, CancellationToken cancellationToken)
        {
            var endpoint = apiEndpont + "/" + login + "/subscription";

            return await ExecuteAsync(HttpMethod.Post, endpoint, null, cancellationToken);
        }

        public async Task<ClientResult> UnsubscribeUserOnAllGroupAsync(string login, CancellationToken cancellationToken)
        {
            var endpoint = apiEndpont + "/" + login + "/subscription";

            return await ExecuteAsync(HttpMethod.Delete, endpoint, null, cancellationToken);
        }

        public async Task<ClientResult> DoNotDisturbUserAsync(string login, CancellationToken cancellationToken)
        {
            var endpoint = apiEndpont + "/" + login + "/dnd";

            return await ExecuteAsync(HttpMethod.Post, endpoint, null, cancellationToken);
        }

        public async Task<ClientResult> RevokeDoNotDisturbUserAsync(string login, CancellationToken cancellationToken)
        {
            var endpoint = apiEndpont + "/" + login + "/dnd";

            return await ExecuteAsync(HttpMethod.Delete, endpoint, null, cancellationToken);
        }

        public async Task<ClientResult<StateResponse>> GetUserDnDStatusAsync(string login, CancellationToken cancellationToken)
        {
            var endpoint = apiEndpont + "/" + login + "/dnd";

            return await ExecuteAsync<StateResponse>(HttpMethod.Get, endpoint, null, cancellationToken);
        }
    }

    internal interface ISubscriptionClient
    {
        public Task<ClientResult> SubscribeUserOnGroupAsync(string login, string groupId, CancellationToken cancellationToken);
        public Task<ClientResult> UnsubscribeUserOnGroupAsync(string login, string groupId, CancellationToken cancellationToken);
        public Task<ClientResult<StateResponse>> GetUserSubscriptionAsync(string login, string groupId, CancellationToken cancellationToken);
        public Task<ClientResult> SubscribeUserOnAllGroupAsync(string login, CancellationToken cancellationToken);
        public Task<ClientResult> UnsubscribeUserOnAllGroupAsync(string login, CancellationToken cancellationToken);
        public Task<ClientResult> DoNotDisturbUserAsync(string login, CancellationToken cancellationToken);
        public Task<ClientResult> RevokeDoNotDisturbUserAsync(string login, CancellationToken cancellationToken);
        public Task<ClientResult<StateResponse>> GetUserDnDStatusAsync(string login, CancellationToken cancellationToken);
    }
}
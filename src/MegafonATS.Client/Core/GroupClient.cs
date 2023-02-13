using MegafonATS.Client.Core.Abstract;
using MegafonATS.Client.Models.Responses.Groups;
using MegafonATS.Client.Results;
using Microsoft.Extensions.Logging;

namespace MegafonATS.Client.Core
{
    public class GroupClient : CRUDClient, IGroupsClient
    {
        const string endpoint = "/groups";

        public GroupClient(HttpClient httpClient, MegafonAtsOptions options, ILogger<ClientBase> logger) : base(httpClient, options, logger) { }

        public async Task<ClientResult<GroupListResponse>> GetGroupsAsync(CancellationToken cancellationToken = default) =>
            await GetAllAsync<GroupListResponse>(endpoint, cancellationToken);

        public async Task<ClientResult<GroupResponse>> GetGroupAsync(string name, CancellationToken cancellationToken = default) =>
            await GetAsync<GroupResponse>(endpoint + "/" + name, cancellationToken);
    }

    public interface IGroupsClient
    {
        Task<ClientResult<GroupListResponse>> GetGroupsAsync(CancellationToken cancellationToken = default);
        Task<ClientResult<GroupResponse>> GetGroupAsync(string name, CancellationToken cancellationToken = default);
    }
}
using MegafonATS.Models.Client.Requests;
using MegafonATS.Models.Client.Responses;

namespace MegafonATS.Client.Core
{
    public class GroupClient : CRUDClient, IGroupsClient
    {
        const string endpoint = "/groups";
        public GroupClient(HttpClient httpClient, MegafonAtsOptions options) : base(httpClient, options)
        {
        }

        public async Task<GroupListModel> GetGroupsAsync(CancellationToken cancellationToken = default) =>
            await GetAllAsync<GroupListModel>(endpoint, cancellationToken);


        public async Task<GroupResponse> GetGroupAsync(string name, CancellationToken cancellationToken = default) =>
            await GetAsync<GroupResponse>(endpoint + "/" + name, cancellationToken);

        public Task<GroupResponse> AddGroupsAsync(AddUserRequest request, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task DeleteGroupsAsync(string login, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<GroupResponse> UpdateGroupsAsync(AddUserRequest request, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateUsersInGroupAsync(string groupId, ChangeUsersInGroupRequest request, CancellationToken cancellationToken = default)
        {
            var apiEndpoint = endpoint + "/" + groupId + "/users";
            var result = await ExecuteAsync(HttpMethod.Post, apiEndpoint, request, cancellationToken);
        }
    }

    public interface IGroupsClient
    {
        Task<GroupListModel> GetGroupsAsync(CancellationToken cancellationToken = default);
        Task<GroupResponse> GetGroupAsync(string name, CancellationToken cancellationToken = default);
        Task<GroupResponse> AddGroupsAsync(AddUserRequest request, CancellationToken cancellationToken = default);
        Task<GroupResponse> UpdateGroupsAsync(AddUserRequest request, CancellationToken cancellationToken = default);
        Task DeleteGroupsAsync(string name, CancellationToken cancellationToken = default);

        Task UpdateUsersInGroupAsync(string groupId, ChangeUsersInGroupRequest request, CancellationToken cancellationToken = default);
    }
}

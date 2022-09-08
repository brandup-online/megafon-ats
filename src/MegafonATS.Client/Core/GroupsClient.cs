using MegafonATS.Models.Client.Requests;
using MegafonATS.Models.Client.Responses;

namespace MegafonATS.Client.Core
{
    public class GroupsClient : MegafonAtsClientBase, IGroupsClient
    {
        public GroupsClient(HttpClient httpClient, MegafonAtsOptions options) : base(httpClient, options)
        {
        }

        public Task<GroupResponse> AddGroupsAsync(AddUserRequest request, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task DeleteGroupsAsync(string login, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IList<GroupResponse>> GetGroupsAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<GroupResponse> GetGroupsAsync(string login, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<GroupResponse> UpdateGroupsAsync(AddUserRequest request, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }

    public interface IGroupsClient
    {
        Task<IList<GroupResponse>> GetGroupsAsync(CancellationToken cancellationToken = default);
        Task<GroupResponse> GetGroupsAsync(string login, CancellationToken cancellationToken = default);
        Task<GroupResponse> AddGroupsAsync(AddUserRequest request, CancellationToken cancellationToken = default);
        Task<GroupResponse> UpdateGroupsAsync(AddUserRequest request, CancellationToken cancellationToken = default);
        Task DeleteGroupsAsync(string login, CancellationToken cancellationToken = default);
    }
}

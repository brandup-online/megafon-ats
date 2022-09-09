using MegafonATS.Models.Client.Requests;
using MegafonATS.Models.Client.Responses;

namespace MegafonATS.Client.Core
{
    public class UserClient : CRUDClient, IUsersClient
    {
        const string userUri = "/users";
        public UserClient(HttpClient httpClient, MegafonAtsOptions options) : base(httpClient, options)
        {
        }

        public async Task<UserResponse> AddUserAsync(AddUserRequest request, CancellationToken cancellationToken = default) =>
            await CreateAsync<UserResponse>(userUri, request, cancellationToken);


        public async Task<UserListModel> GetUsersAsync(CancellationToken cancellationToken = default) =>
            await GetAllAsync<UserListModel>(userUri, cancellationToken);


        public async Task<UserResponse> GetUserAsync(string login, CancellationToken cancellationToken = default) =>
            await GetAsync<UserResponse>(userUri + "/" + login, cancellationToken);


        public async Task<UserResponse> UpdateUserAsync(string login, UpdateUserRequest request, CancellationToken cancellationToken = default) =>
            await UpdateAsync<UserResponse>(userUri + "/" + login, request, cancellationToken);

        public async Task DeleteUserAsync(string login, CancellationToken cancellationToken = default) =>
            await DeleteAsync(userUri + "/" + login, cancellationToken);

    }

    public interface IUsersClient
    {
        #region /users

        Task<UserListModel> GetUsersAsync(CancellationToken cancellationToken = default);
        Task<UserResponse> GetUserAsync(string login, CancellationToken cancellationToken = default);
        Task<UserResponse> AddUserAsync(AddUserRequest request, CancellationToken cancellationToken = default);
        Task<UserResponse> UpdateUserAsync(string login, UpdateUserRequest request, CancellationToken cancellationToken = default);
        Task DeleteUserAsync(string login, CancellationToken cancellationToken = default);

        #endregion

        #region /users/{login}/subscription

        #endregion
    }
}

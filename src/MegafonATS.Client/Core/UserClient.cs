using MegafonATS.Client.Core.Abstract;
using MegafonATS.Client.Results;
using MegafonATS.Models.Client.Responses.Users;
using Microsoft.Extensions.Logging;

namespace MegafonATS.Client.Core
{
    public class UserClient : CRUDClient, IUsersClient
    {
        const string userUri = "/users";
        public UserClient(HttpClient httpClient, MegafonAtsOptions options, ILogger<ClientBase> logger) : base(httpClient, options, logger)
        {
        }

        public async Task<ClientResult<UserListResponse>> GetUsersAsync(CancellationToken cancellationToken = default) =>
            await GetAllAsync<UserListResponse>(userUri, cancellationToken);

        public async Task<ClientResult<UserResponse>> GetUserAsync(string login, CancellationToken cancellationToken = default) =>
            await GetAsync<UserResponse>(userUri + "/" + login, cancellationToken);
    }

    public interface IUsersClient
    {
        #region /users

        Task<ClientResult<UserListResponse>> GetUsersAsync(CancellationToken cancellationToken = default);
        Task<ClientResult<UserResponse>> GetUserAsync(string login, CancellationToken cancellationToken = default);

        #endregion
    }
}

using MegafonATS.Models.Client.Requests;
using MegafonATS.Models.Client.Responses;

namespace MegafonATS.Client.Core
{
    public class UsersClient : MegafonAtsClientBase, IUsersClient
    {
        const string userUri = "/users";
        public UsersClient(HttpClient httpClient, MegafonAtsOptions options) : base(httpClient, options)
        {
        }

        public Task<UserResponse> AddUsersAsync(AddUserRequest request, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
        public async Task<UserListModel> GetUsersAsync(CancellationToken cancellationToken = default)
        {
            var method = userUri;
            var result = await ExecuteGetAsync<UserListModel>(method, cancellationToken);

            if (!result.IsSuccess)
                throw new Exception(result.Error); //Плохо: если падаем то падать надо сразу

            return result.Result;
        }

        public async Task<UserResponse> GetUserAsync(string login, CancellationToken cancellationToken = default)
        {
            var method = userUri + '/' + login;
            var result = await ExecuteGetAsync<UserResponse>(method, cancellationToken);

            if (!result.IsSuccess)
                throw new Exception(result.Error); //Плохо: если падаем то падать надо сразу

            return result.Result;
        }


        public Task<UserResponse> UpdateUsersAsync(AddUserRequest request, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUsersAsync(string login, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

    }

    public interface IUsersClient
    {
        #region /users

        Task<UserListModel> GetUsersAsync(CancellationToken cancellationToken = default);
        Task<UserResponse> GetUserAsync(string login, CancellationToken cancellationToken = default);
        Task<UserResponse> AddUsersAsync(AddUserRequest request, CancellationToken cancellationToken = default);
        Task<UserResponse> UpdateUsersAsync(AddUserRequest request, CancellationToken cancellationToken = default);
        Task DeleteUsersAsync(string login, CancellationToken cancellationToken = default);

        #endregion

        #region /users/{login}/subscription

        #endregion
    }
}

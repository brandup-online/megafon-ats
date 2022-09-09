using MegafonATS.Client.Core;
using MegafonATS.Models.Client.Requests;

namespace MegafonATS.Client
{
    public class UserClientTest : ClientTestBase
    {
        [Fact]
        public async Task GetUsersAsync_Success()
        {
            var client = CreateClient<UserClient>(); //сделать Client disposible
            var result = await client.GetUsersAsync(default);

            Assert.NotNull(result);
            Assert.NotEmpty(result.Items);
        }

        [Fact]
        public async Task GetUserAsync_Success()
        {
            var client = CreateClient<UserClient>();//сделать Client disposible
            var result = await client.GetUserAsync("admin", default);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task AddUserAsync_Success()
        {
            var client = CreateClient<UserClient>();//сделать Client disposible

            var addUserRequest = new AddUserRequest
            {
                Name = "dimaSH",
                Login = "dima1"
            };

            var result = await client.AddUserAsync(addUserRequest, default);

            Assert.NotNull(result);

            Assert.Equal("dimaSH", result.Name);
        }

        [Fact]
        public async Task UpdateUserAsync_Success()
        {
            var client = CreateClient<UserClient>();//сделать Client disposible

            var addUserRequest = new UpdateUserRequest
            {
                Name = "dima"
            };

            await client.UpdateUserAsync("dima1", addUserRequest, default);


            var result = await client.GetUserAsync("u704", default);
            Assert.NotNull(result);

            Assert.Equal("dima", result.Name);
        }

        [Fact]
        public async Task DeleteUserAsync_Success()
        {
            var client = CreateClient<UserClient>();//сделать Client disposible

            var addUserRequest = new AddUserRequest
            {
                Name = "dimaSH",
            };

            await client.DeleteUserAsync("dima1", default);

            var result = await client.GetUsersAsync(default);

            Assert.NotNull(result);
            Assert.NotEmpty(result.Items);
        }
    }
}

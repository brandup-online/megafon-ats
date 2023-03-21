using MegafonATS.Client.Core;

namespace MegafonATS.Client
{
    public class UserClientTest : ClientTestBase
    {
        [Fact]

        public async Task GetUsersAsync_Success()
        {
            var client = CreateClient<UserClient>();
            var result = await client.GetUsersAsync(default);

            Assert.True(result.IsSuccess);
            Assert.NotEmpty(result.Data.Items);
        }

        [Fact]
        public async Task GetUserAsync_Success()
        {
            var client = CreateClient<UserClient>();
            var result = await client.GetUserAsync(Data.UserName, default);

            Assert.True(result.IsSuccess);
            Assert.Equal(Data.UserName, result.Data.Login);
        }
    }
}
using MegafonATS.Client.Core;
using MegafonATS.Models.Enums;

namespace MegafonATS.Client
{
    public class UserClientTest : ClientTestBase
    {
        [Fact]

        public async Task GetUsersAsync_Success()
        {
            var client = CreateClient<UserClient>();
            var result = await client.GetUsersAsync(default);

            Assert.NotNull(result);
            Assert.NotEmpty(result.Data.Items);
        }

        [Fact]
        public async Task GetUserAsync_Success()
        {
            var client = CreateClient<UserClient>();
            var result = await client.GetUserAsync("admin", default);

            Assert.NotNull(result);
            Assert.Equal(UserRole.Admin, result.Data.Role);
        }


    }
}

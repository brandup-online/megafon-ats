using MegafonATS.Client.Core;

namespace MegafonATS.Client
{
    public class GroupsClientTest : ClientTestBase
    {
        [Fact]
        public async Task GetGroupsAsync_Success()
        {
            var client = CreateClient<GroupClient>();
            var result = await client.GetGroupsAsync(default);

            Assert.NotNull(result);
            Assert.NotEmpty(result.Data.Items);
        }

        [Fact]
        public async Task GetGroupAsync_Success()
        {
            var client = CreateClient<GroupClient>();
            var result = await client.GetGroupAsync("_g6978081595918316104", default);

            Assert.NotNull(result);
            Assert.NotEmpty(result.Data.Users);
        }
    }
}
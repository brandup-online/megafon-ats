using MegafonATS.Client.Core;
using MegafonATS.Models.Client.Requests;

namespace MegafonATS.Client
{
    public class GroupsClientTest : ClientTestBase
    {
        [Fact]
        public async Task GetGroupsAsync_Success()
        {
            var client = CreateClient<GroupClient>(); //сделать Client disposible
            var result = await client.GetGroupsAsync(default);

            Assert.NotNull(result);
            Assert.NotEmpty(result.Items);
        }

        [Fact]
        public async Task GetGroupAsync_Success()
        {
            var client = CreateClient<GroupClient>(); //сделать Client disposible
            var result = await client.GetGroupAsync("_g6978081595918316104", default);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task UpdateUsersInGroup_Success()
        {
            var client = CreateClient<GroupClient>(); //сделать Client disposible
            var request = new ChangeUsersInGroupRequest
            {
                AddUsers = new[]
                {
                    "dima1"
                },
            };

            await client.UpdateUsersInGroupAsync("_g6978081595918316104", request, default);

            var result = await client.GetGroupAsync("_g6978081595918316104", default);

            Assert.NotNull(result);
        }
    }
}

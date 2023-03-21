using MegafonATS.Client.Core;

namespace MegafonATS.Client
{
    public class GroupsClientTest : ClientTestBase
    {
        GroupClient client;

        protected override Task OnInitializeAsync(IServiceProvider serviceProvider)
        {
            client = CreateClient<GroupClient>();
            return base.OnInitializeAsync(serviceProvider);
        }

        [Fact]
        public async Task GetGroupsAsync_Success()
        {
            var result = await client.GetGroupsAsync(default);

            Assert.True(result.IsSuccess);
            Assert.NotEmpty(result.Data.Items);
            Assert.Equal(Data.GroupsCount, result.Data.Items.Count);
        }

        [Fact]
        public async Task GetGroupAsync_Success()
        {
            var result = await client.GetGroupAsync(Data.GroupId, default);

            Assert.True(result.IsSuccess);
            Assert.Equal(Data.GroupId, result.Data.Id);
            Assert.NotEmpty(result.Data.Users);
        }
    }
}
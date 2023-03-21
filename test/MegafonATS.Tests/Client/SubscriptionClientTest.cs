using MegafonATS.Client.Core;

namespace MegafonATS.Client
{
    public class SubscriptionClientTest : ClientTestBase
    {
        SubscriptionClient client;
        protected override Task OnInitializeAsync(IServiceProvider services)
        {
            client = CreateClient<SubscriptionClient>();
            return base.OnInitializeAsync(services);
        }

        [Fact]
        public async Task GetUserSubscription_Success()
        {
            var result = await client.GetUserSubscriptionAsync(Data.UserName, Data.GroupId, default);

            Assert.True(result.IsSuccess);
            Assert.True(result.Data.State);
        }

        [Fact]
        public async Task SubscribeUserOnGroup_Success()
        {
            await client.SubscribeUserOnGroupAsync(Data.UserName, Data.GroupId, default);

            var result = await client.GetUserSubscriptionAsync(Data.UserName, Data.GroupId, default);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task UnsubscribeUserOnGroup_Success()
        {
            await client.UnsubscribeUserOnGroupAsync(Data.UserName, Data.GroupId, default);

            var result = await client.GetUserSubscriptionAsync(Data.UserName, Data.GroupId, default);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task SubscribeUserOnAllGroup_Success()
        {
            await client.SubscribeUserOnAllGroupAsync(Data.UserName, default);

            var result = await client.GetUserSubscriptionAsync(Data.UserName, Data.GroupId, default);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task UnsubscribeUserOnAllGroup_Success()
        {
            await client.UnsubscribeUserOnAllGroupAsync(Data.UserName, default);

            var result = await client.GetUserSubscriptionAsync(Data.UserName, Data.GroupId, default);

            Assert.True(result.IsSuccess);
        }
        [Fact]
        public async Task GetUserDnDStatus_Success()
        {
            var result = await client.GetUserDnDStatusAsync(Data.UserName, default);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task DoNotDisturbUser_Success()
        {

            var result = await client.DoNotDisturbUserAsync(Data.UserName, default);
            Assert.True(result.IsSuccess);

            var statusResult = await client.GetUserDnDStatusAsync(Data.UserName, default);
            Assert.True(statusResult.IsSuccess);
        }

        [Fact]
        public async Task RevokeDoNotDisturbUser_Success()
        {
            await client.RevokeDoNotDisturbUserAsync(Data.UserName, default);

            var result = await client.GetUserDnDStatusAsync(Data.UserName, default);

            Assert.True(result.IsSuccess);
        }
    }
}

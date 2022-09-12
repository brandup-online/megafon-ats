﻿using MegafonATS.Client.Core;

namespace MegafonATS.Client
{
    public class SubscriptionClientTest : ClientTestBase
    {
        [Fact]
        public async Task GetUserSubscription_Success()
        {
            var client = CreateClient<SubscriptionClient>();
            var login = "lida";
            var groupId = "_g6978081595918316104";

            var result = await client.GetUserSubscriptionAsync(login, groupId, default);

            Assert.True(result.IsSuccess);
            Assert.True(result.Data.State);
        }

        [Fact]
        public async Task SubscribeUserOnGroup_Success()
        {
            var client = CreateClient<SubscriptionClient>();
            var login = "lida";
            var groupId = "_g6978081595918316104";

            await client.SubscribeUserOnGroupAsync(login, groupId, default);

            var result = await client.GetUserSubscriptionAsync(login, groupId, default);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task UnsubscribeUserOnGroup_Success()
        {
            var client = CreateClient<SubscriptionClient>();
            var login = "lida";
            var groupId = "_g6978081595918316104";

            await client.UnsubscribeUserOnGroupAsync(login, groupId, default);

            var result = await client.GetUserSubscriptionAsync(login, groupId, default);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task SubscribeUserOnAllGroup_Success()
        {
            var client = CreateClient<SubscriptionClient>();

            var login = "lida";
            var groupId = "_g6978081595918316104";

            await client.SubscribeUserOnAllGroupAsync(login, default);

            var result = await client.GetUserSubscriptionAsync(login, groupId, default);

            Assert.True(result.IsSuccess);
        }
        [Fact]
        public async Task UnsubscribeUserOnAllGroup_Success()
        {
            var client = CreateClient<SubscriptionClient>();

            var login = "lida";
            var groupId = "_g6978081595918316104";

            await client.UnsubscribeUserOnAllGroupAsync(login, default);

            var result = await client.GetUserSubscriptionAsync(login, groupId, default);

            Assert.True(result.IsSuccess);
        }
        [Fact]
        public async Task GetUserDnDStatus_Success()
        {
            var client = CreateClient<SubscriptionClient>();

            var login = "lida";
            var result = await client.GetUserDnDStatusAsync(login, default);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task DoNotDisturbUser_Success()
        {
            var client = CreateClient<SubscriptionClient>();

            var login = "lida";
            await client.DoNotDisturbUserAsync(login, default);

            var result = await client.GetUserDnDStatusAsync(login, default);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task RevokeDoNotDisturbUser_Success()
        {
            var client = CreateClient<SubscriptionClient>();

            var login = "lida";
            await client.RevokeDoNotDisturbUserAsync(login, default);

            var result = await client.GetUserDnDStatusAsync(login, default);

            Assert.True(result.IsSuccess);
        }
    }
}

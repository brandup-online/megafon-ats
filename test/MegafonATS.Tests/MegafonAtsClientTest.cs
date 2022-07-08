﻿using MefafonATS.Model.ClientModels;
using MefafonATS.Model.Extensions;
using MefafonATS.Model.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MefafonATS.Model.Tests
{
    public class MegafonAtsClientTest
    {
        readonly IServiceProvider serviceProvider;
        IConfiguration config;
        readonly MegafonAtsOptions options = new MegafonAtsOptions();
        public MegafonAtsClientTest()
        {


            config = new ConfigurationBuilder().AddUserSecrets(typeof(MegafonAtsClientTest).Assembly).Build();
            var services = new ServiceCollection();

            options.AtsName = config["MegafonAts:Options:AtsName"];
            options.Token = config["MegafonAts:Options:Token"];

            services.AddMegafonAtsClient(options =>
            {

            });

            serviceProvider = services.BuildServiceProvider();


        }
        async Task<IMegafonAtsClient> CreateClient()
        {
            var factory = serviceProvider.GetRequiredService<IMegafonClientFactoryService>();
            return await factory.CreateAsync(options);
        }
        [Fact]
        public async Task TestATS_accounts()
        {
            var client = await CreateClient();
            var result = await client.AccountsAsync();
            foreach (var account in result.Result)
            {
                Assert.NotNull(account.Name);
                Assert.NotNull(account.RealName);
                Assert.NotNull(account.Telnum);
                Assert.NotNull(account.Ext);
            }
        }
        [Fact]
        public async Task TestATS_groups()
        {
            var client = await CreateClient();
            var result = await client.GroupsAsync();

            foreach (var account in result.Result)
            {
                Assert.NotNull(account.Id);
                Assert.NotNull(account.RealName);

            }
        }


        [Fact]
        public async Task TestATS_historyPeriod()
        {
            var client = await CreateClient();
            var result = await client.HistoryAsync(EPeriod.today, ECallType.All);

            foreach (var str in result.Result)
            {
                Assert.NotNull(str.Account);
                Assert.NotNull(str.Start);
                Assert.NotNull(str.UID);
                Assert.NotNull(str.Record);
                Assert.NotNull(str.Via);
                Assert.NotNull(str.Wait);
                Assert.NotNull(str.Client);
            }
        }

        [Fact]
        public async Task TestATS_historyStartEnd()
        {
            var client = await CreateClient();
            var result = await client.HistoryAsync(new DateTime(2022, 01, 01, 01, 00, 00), DateTime.Now, ECallType.All);

            foreach (var str in result.Result)
            {
                Assert.NotNull(str.Account);
                Assert.NotNull(str.Start);
                Assert.NotNull(str.UID);
                Assert.NotNull(str.Record);
                Assert.NotNull(str.Via);
                Assert.NotNull(str.Wait);
                Assert.NotNull(str.Client);
            }
        }

        [Fact]
        public async Task TestATS_subscribeOnCalls()
        {
            var client = await CreateClient();
            var result = await client.SubscribeOnCallsAsync(config["MegafonAts:TestUserName"], config["MegafonAts:TestGroupId"], ESubscriptionStatus.On);

            Assert.True(result.IsSuccess);
        }
        [Fact]
        public async Task TestATS_subscriptionStatus()
        {
            var client = await CreateClient();
            var result = await client.SubscriptionStatusAsync(config["MegafonAts:TestUserName"], config["MegafonAts:TestGroupId"]);

            Assert.True(result.Result.Status == ESubscriptionStatus.On);
        }
        [Fact]
        public async Task TestATS_set_dnd()
        {
            var client = await CreateClient();
            var result = await client.SetDnDAsync(config["MegafonAts:TestUserName"], false);

            Assert.True(result.IsSuccess);
        }
        [Fact]
        public async Task TestATS_get_dnd()
        {
            var client = await CreateClient();
            var result = await client.GetDnDAsync(config["MegafonAts:TestUserName"]);

            Assert.True(result.IsSuccess);
        }

    }
}

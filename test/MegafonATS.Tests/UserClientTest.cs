using MegafonATS.Client;
using MegafonATS.Client.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MegafonATS
{
    public class UserClientTest
    {
        readonly IServiceProvider serviceProvider;
        readonly IConfiguration config;
        readonly MegafonAtsOptions options = new MegafonAtsOptions();

        public UserClientTest()
        {
            config = new ConfigurationBuilder().AddUserSecrets(typeof(MegafonAtsClientTest).Assembly).Build();
            var services = new ServiceCollection();

            options.Name = config["MegafonAts:Options:AtsName"];
            options.Key = config["MegafonAts:Options:Token"];

            services.AddMegafonAtsClient();

            serviceProvider = services.BuildServiceProvider();
        }

        UsersClient CreateUserClient()
        {
            var factory = serviceProvider.GetRequiredService<IMegafonAtsClientFactory>();
            return factory.CreateUserClient(options);
        }

        [Fact]
        public async Task GetUsersAsync_Success()
        {
            var client = CreateUserClient(); //сделать Client disposible
            var result = await client.GetUsersAsync(default);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetUserAsync_Success()
        {
            var client = CreateUserClient(); //сделать Client disposible
            var result = await client.GetUserAsync("admin", default);

            Assert.NotNull(result);
        }
    }
}

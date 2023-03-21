using MegafonATS.Client.Core.Abstract;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MegafonATS.Client
{
    public abstract class ClientTestBase : IAsyncLifetime
    {
        readonly ServiceProvider serviceProvider;
        readonly IConfiguration config;
        readonly MegafonAtsOptions options = new();

        protected TestData Data { get; private set; }

        public ClientTestBase()
        {
            config = new ConfigurationBuilder().AddUserSecrets(typeof(ClientTestBase).Assembly).Build();

            TestData testData = new();
            config.GetSection("MegafonAts:TestData").Bind(testData);
            Data = testData;

            var services = new ServiceCollection();

            options.Name = config["MegafonAts:Options:AtsName"];
            options.Key = config["MegafonAts:Options:Token"];

            services.AddMegafonAtsClientFactory();
            services.AddLogging();

            serviceProvider = services.BuildServiceProvider();
        }

        #region IAsyncLifetime members

        public async Task InitializeAsync()
        {
            await OnInitializeAsync(serviceProvider);
        }

        public async Task DisposeAsync()
        {
            await serviceProvider.DisposeAsync();
        }

        #endregion

        #region Virtual members

        protected virtual Task OnInitializeAsync(IServiceProvider services)
        {
            return Task.CompletedTask;
        }

        protected virtual Task OnDisposeAsync(IServiceProvider services)
        {
            return Task.CompletedTask;
        }

        #endregion

        protected Type CreateClient<Type>() where Type : ClientBase
        {
            var factory = serviceProvider.GetRequiredService<IMegafonAtsClientFactory>();
            return factory.Create<Type>(options);
        }

        protected class TestData
        {
            public string UserName { get; set; }
            public string GroupId { get; set; }
            public int GroupsCount { get; set; }
        }
    }
}

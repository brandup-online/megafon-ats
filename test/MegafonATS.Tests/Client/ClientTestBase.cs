using MegafonATS.Client.Core.Abstract;
using MegafonATS.Client.Factory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MegafonATS.Client
{
    public abstract class ClientTestBase
    {
        readonly IServiceProvider serviceProvider;
        readonly IConfiguration config;
        readonly MegafonAtsOptions options = new();

        public ClientTestBase()
        {
            config = new ConfigurationBuilder().AddUserSecrets(typeof(ClientTestBase).Assembly).Build();
            var services = new ServiceCollection();

            options.Name = config["MegafonAts:Options:AtsName"];
            options.Key = config["MegafonAts:Options:Token"];

            services.AddMegafonAtsClientFactory();
            services.AddLogging();

            serviceProvider = services.BuildServiceProvider();
        }

        protected Type CreateClient<Type>() where Type : ClientBase
        {
            var factory = serviceProvider.GetRequiredService<IMegafonAtsClientFactory>();
            return factory.Create<Type>(options);
        }
    }
}

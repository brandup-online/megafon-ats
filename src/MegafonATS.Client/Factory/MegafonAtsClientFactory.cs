using MegafonATS.Client.Core.Abstract;
using Microsoft.Extensions.Logging;

namespace MegafonATS.Client.Factory
{
    public class MegafonAtsClientFactory : IMegafonAtsClientFactory
    {
        readonly IHttpClientFactory httpClientFactory;
        readonly ILogger<ClientBase> logger;

        public MegafonAtsClientFactory(IHttpClientFactory httpClientFactory, ILogger<ClientBase> logger)
        {
            this.httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Type Create<Type>(MegafonAtsOptions options) where Type : ClientBase
        {
            var client = httpClientFactory.CreateClient();
            return Activator.CreateInstance(typeof(Type), new object[] { client, options, logger }) as Type;
        }
    }
}
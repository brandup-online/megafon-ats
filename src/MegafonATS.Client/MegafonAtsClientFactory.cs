using MegafonATS.Client.Core.Abstract;
using Microsoft.Extensions.Logging;

namespace MegafonATS.Client
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

        public TClient Create<TClient>(MegafonAtsOptions options)
            where TClient : ClientBase
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            var client = httpClientFactory.CreateClient();
            return Activator.CreateInstance(typeof(TClient), new object[] { client, options, logger }) as TClient;
        }
    }
}
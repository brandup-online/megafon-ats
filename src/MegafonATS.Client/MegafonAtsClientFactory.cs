using MegafonATS.Client.Core;

namespace MegafonATS.Client
{
    public class MegafonAtsClientFactory : IMegafonAtsClientFactory
    {
        readonly IHttpClientFactory httpClientFactory;

        public MegafonAtsClientFactory(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }

        public MegafonAtsClientBase Create(MegafonAtsOptions options)
        {
            throw new NotImplementedException();
        }

        public UsersClient CreateUserClient(MegafonAtsOptions options)
        {
            var client = httpClientFactory.CreateClient();
            return new UsersClient(client, options);
        }

    }
}
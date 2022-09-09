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

        public Type Create<Type>(MegafonAtsOptions options) where Type : ClientBase
        {
            var client = httpClientFactory.CreateClient();
            return Activator.CreateInstance(typeof(Type), new object[] { client, options }) as Type;
        }

        public UserClient CreateUserClient(MegafonAtsOptions options)
        {
            var client = httpClientFactory.CreateClient();
            return new UserClient(client, options);
        }

    }
}
namespace MegafonATS.Client
{
    public class MegafonAtsClientFactory : IMegafonAtsClientFactory
    {
        readonly IHttpClientFactory httpClientFactory;

        public MegafonAtsClientFactory(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }

        public IMegafonAtsClient Create(MegafonAtsOptions atsOptions)
        {
            var client = httpClientFactory.CreateClient();
            return new MegafonAtsClient(client, atsOptions);
        }
    }
}
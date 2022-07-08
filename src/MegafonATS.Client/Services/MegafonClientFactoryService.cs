namespace MefafonATS.Model.Services
{
    public class MegafonClientFactoryService : IMegafonClientFactoryService
    {
        IHttpClientFactory httpClientFactory;
        public MegafonClientFactoryService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        public Task<IMegafonAtsClient> CreateAsync(MegafonAtsOptions options, CancellationToken cancellationToken = default)
        {
            var client = httpClientFactory.CreateClient();
            IMegafonAtsClient megafonAtsClient = new MegafonAtsClient(client, options);
            return Task.FromResult(megafonAtsClient);
        }


    }
}

namespace MefafonATS.Model.Services
{
    public interface IMegafonClientFactoryService
    {
        public Task<IMegafonAtsClient> CreateAsync(MegafonAtsOptions options, CancellationToken cancellationToken = default);
    }
}

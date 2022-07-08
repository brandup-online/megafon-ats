namespace MegafonATS.Client
{
    public interface IMegafonAtsClientFactory
    {
        IMegafonAtsClient Create(MegafonAtsOptions options);
    }
}

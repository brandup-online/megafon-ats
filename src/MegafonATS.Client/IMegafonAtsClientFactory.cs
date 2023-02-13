using MegafonATS.Client.Core.Abstract;

namespace MegafonATS.Client
{
    public interface IMegafonAtsClientFactory
    {
        TClient Create<TClient>(MegafonAtsOptions options)
            where TClient : ClientBase;
    }
}
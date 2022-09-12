using MegafonATS.Client.Core.Abstract;

namespace MegafonATS.Client.Factory
{
    public interface IMegafonAtsClientFactory
    {
        Type Create<Type>(MegafonAtsOptions options) where Type : ClientBase;
    }
}

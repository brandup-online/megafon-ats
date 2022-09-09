using MegafonATS.Client.Core;

namespace MegafonATS.Client
{
    public interface IMegafonAtsClientFactory
    {
        Type Create<Type>(MegafonAtsOptions options) where Type : ClientBase;
        UserClient CreateUserClient(MegafonAtsOptions options);
    }
}

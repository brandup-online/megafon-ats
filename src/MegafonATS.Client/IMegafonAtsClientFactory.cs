using MegafonATS.Client.Core;

namespace MegafonATS.Client
{
    public interface IMegafonAtsClientFactory
    {
        MegafonAtsClientBase Create(MegafonAtsOptions options);
        UsersClient CreateUserClient(MegafonAtsOptions options);
    }
}

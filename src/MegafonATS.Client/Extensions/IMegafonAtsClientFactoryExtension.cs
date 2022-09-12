using MegafonATS.Client.Core;
using MegafonATS.Client.Factory;

namespace MegafonATS.Client
{
    public static class IMegafonAtsClientFactoryExtension
    {
        public static CallClient CreateCallClient(this IMegafonAtsClientFactory megafonAtsClientFactory, MegafonAtsOptions options) =>
            megafonAtsClientFactory.Create<CallClient>(options);
        public static GroupClient CreateGroupsClient(this IMegafonAtsClientFactory megafonAtsClientFactory, MegafonAtsOptions options) =>
            megafonAtsClientFactory.Create<GroupClient>(options);
        public static HistoryClient CreateHistoryClient(this IMegafonAtsClientFactory megafonAtsClientFactory, MegafonAtsOptions options) =>
            megafonAtsClientFactory.Create<HistoryClient>(options);
        public static SubscriptionClient CreateSubscriptionClient(this IMegafonAtsClientFactory megafonAtsClientFactory, MegafonAtsOptions options) =>
            megafonAtsClientFactory.Create<SubscriptionClient>(options);
        public static UserClient CreateUserClient(this IMegafonAtsClientFactory megafonAtsClientFactory, MegafonAtsOptions options) =>
            megafonAtsClientFactory.Create<UserClient>(options);

    }
}

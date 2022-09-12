using MegafonATS.Client.Factory;
using Microsoft.Extensions.DependencyInjection;

namespace MegafonATS.Client
{
    public static class IServiceCollectionExtensions
    {
        public static void AddMegafonAtsClient(this IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddScoped<IMegafonAtsClientFactory, MegafonAtsClientFactory>();
        }
    }
}
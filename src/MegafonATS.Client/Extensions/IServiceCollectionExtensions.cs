using Microsoft.Extensions.DependencyInjection;

namespace MegafonATS.Client
{
    public static class IServiceCollectionExtensions
    {
        public static void AddMegafonAtsClientFactory(this IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddScoped<IMegafonAtsClientFactory, MegafonAtsClientFactory>();
        }
    }
}
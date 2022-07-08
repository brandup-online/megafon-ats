using Microsoft.Extensions.DependencyInjection;

namespace MegafonATS.Webhooks
{
    public static class IServiceCollectionExtensions
    {
        public static void AddMegafonWebHooks<TService>(this IServiceCollection services) where TService : class, IMegafonAtsEvents
        {
            services.AddScoped<IMegafonAtsEvents, TService>();
        }
    }
}
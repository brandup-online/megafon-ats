using MefafonATS.Model.WebhooksModel;
using Microsoft.Extensions.DependencyInjection;

namespace MefafonATS.Webhooks.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddMegafonWebHooks<TService>(this IServiceCollection services) where TService : class, IMegafonAtsEvents
        {
            services.AddTransient<IMegafonAtsEvents, TService>();
        }
    }

}

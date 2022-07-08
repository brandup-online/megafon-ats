using MefafonATS.Model.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MefafonATS.Model.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddMegafonAtsClient(this IServiceCollection services, Action<MegafonAtsOptions> configure)
        {
            services.Configure(configure);
            services.AddHttpClient();

            services.AddScoped<IMegafonClientFactoryService, MegafonClientFactoryService>();

            services.PostConfigure<MegafonAtsOptions>(options =>
            {
                //options.Validation();
            });
        }
    }
}
using MegafonATS.Fakes;
using MegafonATS.Webhooks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace MegafonATS
{
    internal class AgentFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                services.AddMegafonWebHooks<FakeMegafonAtsEvents>();
                services.AddSingleton<FakeMegafonAtsEventsResults>();
            });
        }
    }
}
using MefafonATS.Model.WebhooksModel;
using MefafonATS.Webhooks;
using MefafonATS.Webhooks.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;


namespace MefafonATS.Model.Tests
{
    internal class AgentFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                services.AddMegafonWebHooks<MyTestService>();
            });

        }
    }

    public class MyTestService : IMegafonAtsEvents
    {
        public static HistoryModel History { get; private set; }
        public static ContactModel Contact { get; private set; }
        public static EventModel Event { get; private set; }
        public static RatingModel Rating { get; private set; }

        public MyTestService()
        {

        }
        public Task ContactAsync(ContactModel contact)
        {
            Contact = contact;

            return Task.CompletedTask;
        }

        public Task EventAsync(EventModel _event)
        {
            Event = _event;
            return Task.CompletedTask;
        }

        public Task HistoryAsync(HistoryModel history)
        {
            History = history;
            return Task.CompletedTask;
        }

        public Task RatingAsync(RatingModel rating)
        {
            Rating = rating;
            return Task.CompletedTask;
        }
    }
}

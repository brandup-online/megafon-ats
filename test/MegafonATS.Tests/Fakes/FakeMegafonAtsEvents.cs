using MegafonATS.Webhooks;
using MegafonATS.Webhooks.Models.Requests;
using MegafonATS.Webhooks.Models.Responses;

namespace MegafonATS.Fakes
{
    public class FakeMegafonAtsEvents : IMegafonAtsEvents
    {
        readonly FakeMegafonAtsEventsResults results;

        public FakeMegafonAtsEvents(FakeMegafonAtsEventsResults results)
        {
            this.results = results ?? throw new ArgumentNullException(nameof(results));
        }

        public Task<ContactResponse> ContactAsync(string token, ContactModel contact, CancellationToken cancellationToken = default)
        {
            results.Contact = contact;

            return Task.FromResult(new ContactResponse { ContactName = "Name", Responsible = "Manager" });
        }

        public Task EventAsync(string token, EventModel _event, CancellationToken cancellationToken = default)
        {
            results.Event = _event;
            return Task.CompletedTask;
        }

        public Task HistoryAsync(string token, HistoryModel history, CancellationToken cancellationToken = default)
        {
            results.History = history;

            return Task.CompletedTask;
        }



        public Task RatingAsync(string token, RatingModel rating, CancellationToken cancellationToken = default)
        {
            results.Rating = rating;

            return Task.CompletedTask;
        }
    }

    public class FakeMegafonAtsEventsResults
    {
        public HistoryModel History { get; set; }
        public ContactModel Contact { get; set; }
        public EventModel Event { get; set; }
        public RatingModel Rating { get; set; }
    }
}
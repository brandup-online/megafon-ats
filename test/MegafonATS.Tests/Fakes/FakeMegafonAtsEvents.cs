using MegafonATS.Models.Webhooks;
using MegafonATS.Webhooks;

namespace MegafonATS.Fakes
{
    public class FakeMegafonAtsEvents : IMegafonAtsEvents
    {
        readonly FakeMegafonAtsEventsResults results;

        public FakeMegafonAtsEvents(FakeMegafonAtsEventsResults results)
        {
            this.results = results ?? throw new ArgumentNullException(nameof(results));
        }

        public Task ContactAsync(ContactModel contact)
        {
            results.Contact = contact;

            return Task.CompletedTask;
        }

        public Task EventAsync(EventModel _event)
        {
            results.Event = _event;
            return Task.CompletedTask;
        }

        public Task HistoryAsync(HistoryModel history)
        {
            results.History = history;

            return Task.CompletedTask;
        }

        public Task RatingAsync(RatingModel rating)
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
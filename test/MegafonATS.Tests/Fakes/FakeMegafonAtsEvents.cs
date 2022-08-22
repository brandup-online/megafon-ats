using MegafonATS.Models.Webhooks;
using MegafonATS.Webhooks;

namespace MegafonATS.Fakes
{
    public class FakeMegafonAtsEvents : IMegafonAtsEvents
    {
        readonly FakeMegafonAtsEventsResults results;

        public Task<bool> IsValidTokenAsync(string token, CancellationToken cancellationToken)
        {
            if (results == null)
                return Task.FromResult(false);
            return Task.FromResult(true);
        }

        public FakeMegafonAtsEvents(FakeMegafonAtsEventsResults results)
        {
            this.results = results ?? throw new ArgumentNullException(nameof(results));
        }

        public Task ContactAsync(ContactModel contact, CancellationToken cancellationToken = default)
        {
            results.Contact = contact;

            return Task.CompletedTask;
        }

        public Task EventAsync(EventModel _event, CancellationToken cancellationToken = default)
        {
            results.Event = _event;
            return Task.CompletedTask;
        }

        public Task HistoryAsync(HistoryModel history, CancellationToken cancellationToken = default)
        {
            results.History = history;

            return Task.CompletedTask;
        }



        public Task RatingAsync(RatingModel rating, CancellationToken cancellationToken = default)
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
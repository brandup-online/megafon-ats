using MefafonATS.Model.WebhooksModel;

namespace MefafonATS.Webhooks
{
    public interface IMegafonAtsEvents
    {
        public Task HistoryAsync(HistoryModel history);
        public Task EventAsync(EventModel _event);
        public Task ContactAsync(ContactModel contact);
        public Task RatingAsync(RatingModel rating);
    }
}

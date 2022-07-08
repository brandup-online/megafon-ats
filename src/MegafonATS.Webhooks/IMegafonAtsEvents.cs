using MegafonATS.Models.Webhooks;

namespace MegafonATS.Webhooks
{
    public interface IMegafonAtsEvents
    {
        Task HistoryAsync(HistoryModel history);
        Task EventAsync(EventModel _event);
        Task ContactAsync(ContactModel contact);
        Task RatingAsync(RatingModel rating);
    }
}
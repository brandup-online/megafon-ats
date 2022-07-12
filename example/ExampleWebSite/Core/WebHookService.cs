using MegafonATS.Models.Webhooks;
using MegafonATS.Webhooks;

namespace ExampleWebSite.Core
{
    public class WebHookService : IMegafonAtsEvents
    {
        readonly WebSiteDbContext context;

        public WebHookService(WebSiteDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task ContactAsync(ContactModel contact, string token)
        {
            throw new NotImplementedException();
        }

        public Task EventAsync(EventModel _event, string token) =>
            context.Events.InsertOneAsync(ParseToEventDocument(_event));


        public Task HistoryAsync(HistoryModel history, string token) =>
            context.History.InsertOneAsync(ParseToHistoryDocument(history));

        public Task RatingAsync(RatingModel rating, string token)
        {
            throw new NotImplementedException();
        }

        static HistoryDocument ParseToHistoryDocument(HistoryModel history) =>
            new()
            {
                Id = new Guid(),
                Created = DateTime.UtcNow,
                Type = history.Type,
                User = history.User,
                Ext = history.Ext,
                groupRealName = history.groupRealName,
                Telnum = history.Telnum,
                Phone = history.Phone,
                Diversion = history.Diversion,
                Start = history.Start,
                Duration = history.Duration,
                Callid = history.Callid,
                Link = history.Link,
                Rating = history.Rating,
                Status = history.Status
            };

        static EventDocument ParseToEventDocument(EventModel eventModel) =>
            new()
            {
                Id = new Guid(),
                Created = DateTime.UtcNow,
                Type = eventModel.Type,
                Phone = eventModel.Phone,
                Diversion = eventModel.Diversion,
                User = eventModel.User,
                GroupRealName = eventModel.GroupRealName,
                Ext = eventModel.Ext,
                Telnum = eventModel.Telnum,
                Direction = eventModel.Direction,
                Callid = eventModel.Callid
            };
    }
}
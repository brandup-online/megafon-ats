using MefafonATS.Model.WebhooksModel;
using MefafonATS.Webhooks;

namespace ExampleWebSite.Core
{
    public class WebHookService : IMegafonAtsEvents
    {
        WebSiteDbContext _context;
        public WebHookService(WebSiteDbContext context)
        {
            _context = context;
        }
        public async Task ContactAsync(ContactModel contact)
        {
            throw new NotImplementedException();
        }

        public async Task EventAsync(EventModel _event) =>
            await _context.Events.InsertOneAsync(ParseToEventDocument(_event));


        public async Task HistoryAsync(HistoryModel history) =>
            await _context.History.InsertOneAsync(ParseToHistoryDocument(history));

        public async Task RatingAsync(RatingModel rating)
        {
            throw new NotImplementedException();
        }

        HistoryDocument ParseToHistoryDocument(HistoryModel history) =>
            new HistoryDocument()
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
                Status = history.Status,

            };

        EventDocument ParseToEventDocument(EventModel eventModel) =>
            new EventDocument()
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
                Callid = eventModel.Callid,

            };


    }
}

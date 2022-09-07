using MegafonATS.Models.Webhooks.RequestModels;
using MegafonATS.Models.Webhooks.ResponseModels;
using MegafonATS.Webhooks;
using Microsoft.Extensions.Options;

namespace ExampleWebSite.Core
{
    public class WebHookService : IMegafonAtsEvents
    {
        readonly WebSiteDbContext context;
        readonly MegafonCallBackOptions options;

        public WebHookService(WebSiteDbContext context, IOptions<MegafonCallBackOptions> options)
        {
            this.options = options.Value;
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public Task<bool> IsValidTokenAsync(string token, CancellationToken cancellationToken)
        {
            return token == options.Token ? Task.FromResult(true) : Task.FromResult(false);
        }

        public Task<ContactResponse> ContactAsync(ContactModel contact, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task EventAsync(EventModel _event, CancellationToken cancellationToken = default) =>
            await context.Events.InsertOneAsync(ParseToEventDocument(_event), null, cancellationToken);


        public async Task HistoryAsync(HistoryModel history, CancellationToken cancellationToken = default) =>
            await context.History.InsertOneAsync(ParseToHistoryDocument(history), null, cancellationToken);

        public Task RatingAsync(RatingModel rating, CancellationToken cancellationToken = default)
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
                groupRealName = history.GroupRealName,
                Telnum = history.Telnum,
                Phone = history.Phone,
                Diversion = history.Diversion,
                Start = history.Start,
                Duration = history.Duration,
                Callid = history.CallId,
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
                Callid = eventModel.CallId
            };


    }
}
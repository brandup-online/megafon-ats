using BrandUp.Extensions.Migrations;
using ExampleWebSite.Core;
using MegafonATS.Client;
using MegafonATS.Models.Client;

namespace ExampleWebSite.Migrations
{
    [Setup]
    public class HistoryMigrationSetup : IMigrationHandler
    {
        readonly IWebHooksContext dbContext;
        readonly IMegafonAtsClient megafonAtsClient;

        public HistoryMigrationSetup(IWebHooksContext dbContext, IMegafonAtsClient megafonAtsClient)
        {
            this.dbContext = dbContext;
            this.megafonAtsClient = megafonAtsClient;
        }

        public Task DownAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task UpAsync(CancellationToken cancellationToken = default)
        {
            var result = await megafonAtsClient.HistoryAsync(new DateTime(2022, 01, 01, 01, 00, 00), DateTime.Now, FilterCallType.All, 100);
            foreach (var item in result.Result)
            {
                await dbContext.History.InsertOneAsync(new HistoryDocument()
                {
                    Id = new Guid(),
                    Created = DateTime.Now.ToUniversalTime(),
                    Type = item.Type,
                    User = item.Account,
                    Ext = "0",
                    groupRealName = "0",
                    Telnum = "0",
                    Phone = item.Client,
                    Diversion = item.Via,
                    Start = DateTime.Parse(item.Start).ToUniversalTime(),
                    Duration = Convert.ToInt32(item.Duration),
                    Callid = item.UID,
                    Link = item.Record,
                    Rating = 0,
                    Status = "0"
                });
            }

        }
    }
}
using BrandUp.MongoDB;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace ExampleWebSite.Core
{
    public class WebSiteDbContext : MongoDbContext, IWebHooksContext
    {
        public WebSiteDbContext(MongoDbContextOptions options) : base(options)
        {

        }

        public IMongoCollection<HistoryDocument> History => GetCollection<HistoryDocument>();

        public IMongoCollection<EventDocument> Events => GetCollection<EventDocument>();
    }
    public interface IWebHooksContext
    {
        IMongoCollection<HistoryDocument> History { get; }
        IMongoCollection<EventDocument> Events { get; }
    }
    [Document(CollectionName = "MegasfonAts.Events")]
    public class EventDocument
    {
        [BsonId, BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public Guid Id { get; set; }
        [BsonDateTimeOptions(DateOnly = false, Kind = DateTimeKind.Utc, Representation = MongoDB.Bson.BsonType.DateTime)]
        public DateTime Created { get; set; }
        public string Type { get; set; }
        [BsonRequired]
        public string Phone { get; set; }
        public string Diversion { get; set; }
        [BsonRequired]
        public string User { get; set; }
        public string GroupRealName { get; set; }
        public string Ext { get; set; }
        [BsonRequired]
        public string Telnum { get; set; }
        public string Direction { get; set; }
        public string Callid { get; set; }
    }
    [Document(CollectionName = "MegasfonAts.History")]
    public class HistoryDocument
    {

        [BsonId, BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public Guid Id { get; set; }
        [BsonDateTimeOptions(DateOnly = false, Kind = DateTimeKind.Utc, Representation = MongoDB.Bson.BsonType.DateTime)]
        public DateTime Created { get; set; }
        [BsonRequired]
        public string Type { get; set; }
        [BsonRequired]
        public string User { get; set; }
        public string Ext { get; set; }
        public string groupRealName { get; set; }
        [BsonRequired]
        public string Telnum { get; set; }
        public string Phone { get; set; }
        public string Diversion { get; set; }
        public DateTime Start { get; set; }
        public int Duration { get; set; }
        public string Callid { get; set; }
        public string Link { get; set; }
        public int Rating { get; set; }
        public string Status { get; set; }
    }
}

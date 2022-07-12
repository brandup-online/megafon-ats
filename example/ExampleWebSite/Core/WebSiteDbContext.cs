﻿using BrandUp.MongoDB;
using MegafonATS.Models;
using MegafonATS.Models.Webhooks;
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
        public EventType Type { get; set; }
        [BsonRequired]
        public string Phone { get; set; }
        public string Diversion { get; set; }
        [BsonRequired]
        public string User { get; set; }
        public string GroupRealName { get; set; }
        public string Ext { get; set; }
        [BsonRequired]
        public string Telnum { get; set; }
        public CallDirection Direction { get; set; }
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
        public CallDirection Type { get; set; }
        [BsonRequired]
        public string User { get; set; }
        public string Ext { get; set; }
        public string groupRealName { get; set; }
        [BsonRequired]
        public string Telnum { get; set; }
        [BsonRequired]
        public string Phone { get; set; }
        public string Diversion { get; set; }
        [BsonRequired]
        [BsonDateTimeOptions(DateOnly = false, Kind = DateTimeKind.Utc, Representation = MongoDB.Bson.BsonType.DateTime)]
        public DateTime Start { get; set; }
        [BsonRequired]
        public int Duration { get; set; }
        public string Callid { get; set; }
        public Uri Link { get; set; }
        public int Rating { get; set; }
        [BsonRequired]
        public CallStatus Status { get; set; }
    }
}
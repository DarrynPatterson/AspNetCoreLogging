using MongoDB.Bson.Serialization.Attributes;

namespace AspNetCoreLogging.Logging.MongoCollection.Repository
{
    [BsonIgnoreExtraElements]
    public class LogMongoDocument
    {
        [BsonIgnoreIfNull]
        [BsonElement("entry")]
        public string Entry { get; set; }
    }
}
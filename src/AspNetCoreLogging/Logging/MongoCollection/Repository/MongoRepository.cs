using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace AspNetCoreLogging.Logging.MongoCollection.Repository
{
    public class MongoRepository
    {
        #region Properties

        private MongoConfig Config { get; }

        private MongoClient Client { get; }

        private IMongoDatabase Database
        {
            get
            {
                return Client.GetDatabase(Config.Database);
            }
        }

        private IMongoCollection<LogMongoDocument> Collection
        {
            get
            {
                return Database.GetCollection<LogMongoDocument>(Config.Collection);
            }
        }

        #endregion

        #region Constructor

        public MongoRepository(MongoConfig config)
        {
            Config = config;
            var connectionString = $"mongodb://{Config.UserName}:{Config.Password}@{Config.Host}:{Config.Port}/{Config.Database}";
            Client = new MongoClient(connectionString);

            var conventionPack = new ConventionPack();
            conventionPack.Add(new CamelCaseElementNameConvention());
            ConventionRegistry.Register("camelCase", conventionPack, type => true);
        }

        #endregion

        public async Task Insert(IEnumerable<string> entries)
        {
            await Collection.InsertManyAsync(entries.Select(x => new LogMongoDocument() { Entry = x }));
        }
    }
}
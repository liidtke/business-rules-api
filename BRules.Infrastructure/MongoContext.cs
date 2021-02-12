using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace BRules.Infrastructure
{
    public class MongoContext
    {
        private IMongoDatabase Database { get; set; }
        public IMongoClient MongoClient { get; set; }

        public MongoContext(IDatabaseSettings settings, IMongoClient mongoClient)
        {
            MongoClient = mongoClient;
            Database = MongoClient.GetDatabase(settings.DatabaseName);
            RegisterConventions();
        }

        internal IMongoCollection<T> GetCollection<T>(string name)
        {
            var collection = Database.GetCollection<T>(name);
            return collection;
        }

        private void RegisterConventions()
        {
            var pack = new ConventionPack
        {
            new IgnoreExtraElementsConvention(true)
            //new IgnoreIfDefaultConvention(true)
        };
            ConventionRegistry.Register("My Solution Conventions", pack, t => true);
        }
    }
}
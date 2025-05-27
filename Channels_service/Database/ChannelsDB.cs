using MongoDB.Driver;

namespace Channels_service.Database
{
    public class ChannelsDB
    {
        private readonly string credentials;

        public IMongoDatabase database;

        public ChannelsDB()
        {
            this.credentials = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("credentials")!;
            this.database = new MongoClient(this.credentials).GetDatabase("Server");
        }
    }
}

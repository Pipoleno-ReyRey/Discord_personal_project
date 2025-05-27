using MongoDB.Driver;

namespace Servers_service.Database
{
    public class ServersDb
    {
        public static MongoClient MongoClient()
        {
            string? connection = new ConfigurationBuilder().AddJsonFile("appsettings.json")
                .Build().GetConnectionString("credentials");
            return new MongoClient(connection);
        }
    }
}

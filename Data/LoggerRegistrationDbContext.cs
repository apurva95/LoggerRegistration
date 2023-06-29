using LoggerRegistration.Models;
using MongoDB.Bson;
using MongoDB.Driver;
namespace LoggerRegistration.Data
{
    public class LoggerRegistrationDbContext
    {
        private readonly IMongoDatabase _database;
        public LoggerRegistrationDbContext(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MongoDBAtlas");
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var client = new MongoClient(settings);
            _database = client.GetDatabase("loggerRegistration");
        }

        public IMongoCollection<Registration> Registrations => _database.GetCollection<Registration>("registrations");
    }
}

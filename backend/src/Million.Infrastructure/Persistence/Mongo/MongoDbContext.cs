using Microsoft.Extensions.Options;
using Million.Domain.Entities;
using MongoDB.Driver;

namespace Million.Infrastructure.Persistence.Mongo;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;
    private readonly MongoDbSettings _settings;

    public MongoDbContext(IOptions<MongoDbSettings> settings)
    {
        _settings = settings.Value;
        var client = new MongoClient(_settings.ConnectionString);
        _database = client.GetDatabase(_settings.DatabaseName);
    }

    public virtual IMongoCollection<Property> Properties =>
        _database.GetCollection<Property>(_settings.PropertiesCollectionName);
}

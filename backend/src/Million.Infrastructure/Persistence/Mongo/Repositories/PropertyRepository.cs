using MongoDB.Driver;
using MongoDB.Bson;
using Million.Application.Shared.Contracts.Infrastructure.Persistence.Repositories;
using Million.Domain.Entities;

namespace Million.Infrastructure.Persistence.Mongo.Repositories;

public class PropertyRepository : IPropertyRepository
{
    private readonly IMongoCollection<Property> _collection;

    public PropertyRepository(MongoDbContext context)
    {
        _collection = context.Properties;
    }

    public async Task<(List<Property> Properties, long TotalCount)> GetAllPaginatedAsync(int pageIndex, int pageSize)
    {
        var totalCount = await _collection.CountDocumentsAsync(FilterDefinition<Property>.Empty);

        var properties = await _collection
            .Find(FilterDefinition<Property>.Empty)
            .Skip((pageIndex - 1) * pageSize)
            .Limit(pageSize)
            .ToListAsync();

        return (properties, totalCount);
    }

    public async Task<(List<Property> Properties, long TotalCount)> GetFilteredPaginatedAsync(
        string? name,
        string? address,
        decimal? minPrice,
        decimal? maxPrice,
        int pageIndex,
        int pageSize)
    {
        var builder = Builders<Property>.Filter;
        var filter = builder.Empty;

        if (!string.IsNullOrWhiteSpace(name))
            filter &= builder.Regex(p => p.Name, new BsonRegularExpression(name, "i"));

        if (!string.IsNullOrWhiteSpace(address))
            filter &= builder.Regex(p => p.Address, new BsonRegularExpression(address, "i"));

        if (minPrice.HasValue)
            filter &= builder.Gte(p => p.Price, minPrice.Value);

        if (maxPrice.HasValue)
            filter &= builder.Lte(p => p.Price, maxPrice.Value);

        var totalCount = await _collection.CountDocumentsAsync(filter);

        var properties = await _collection
            .Find(filter)
            .Skip((pageIndex - 1) * pageSize)
            .Limit(pageSize)
            .ToListAsync();

        return (properties, totalCount);
    }

    public async Task<Property?> GetByIdAsync(string id)
    {
        return await _collection.Find(p => p.Id == id).FirstOrDefaultAsync();
    }
}

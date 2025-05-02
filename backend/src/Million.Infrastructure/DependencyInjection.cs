using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Million.Application.Shared.Contracts.Infrastructure.Persistence.Repositories;
using Million.Infrastructure.Persistence.Mongo;
using Million.Infrastructure.Persistence.Mongo.Repositories;

namespace Million.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MongoDbSettings>(configuration.GetSection("MongoDbSettings"));
        services.AddScoped<MongoDbContext>();
        services.AddScoped<IPropertyRepository, PropertyRepository>();

        return services;
    }
}

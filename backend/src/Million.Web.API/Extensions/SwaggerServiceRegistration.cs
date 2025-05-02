using Microsoft.OpenApi.Models;

namespace Million.Web.API.Extensions;

public static class SwaggerServiceRegistration
{
    public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Million API", Version = "v1" });
        });

        return services;
    }
}

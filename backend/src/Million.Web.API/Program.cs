using Million.Application;
using Million.Infrastructure;
using Million.Infrastructure.Persistence.Mongo;
using Million.Web.API.Extensions;
using Million.Web.API.Middleware;
using Million.Web.API.Seeders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var millionOrigins = "_MillionOrigins";

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy(millionOrigins,
                    builder =>
                    {
                        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                    });
});

builder.Services.AddSwaggerServices();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// launch seeder
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<MongoDbContext>();
    await MongoDbSeeder.SeedAsync(context);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseCors(millionOrigins);

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

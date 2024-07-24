using Restaurants.Application;
using Restaurants.Infrastructure;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Host.UseSerilog(
    (context, configuration) => configuration.ReadFrom.Configuration(context.Configuration)
);

var app = builder.Build();
var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<IRestaurantSeeder>();
await seeder.Seed();

app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

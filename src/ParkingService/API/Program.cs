using Infrastructure;
using API.Services;
using ProtosContract;
using Application.Base.Command;
using Infrastructure.Bus.Command;
using Application.Base.Query;
using Infrastructure.Bus.Query;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddGrpc();
builder.Services.AddGrpcClient<Vehicles.VehiclesClient>(options =>
{
    options.Address = new Uri("https://localhost:7154");
});
builder.Services.AddInfrastructure(configuration);
builder.Services.AddCommandHandlers(typeof(Command).Assembly);
builder.Services.AddScoped<ICommandBus, InMemoryCommandBus>();
builder.Services.AddQueryHandlers(typeof(Query).Assembly);
builder.Services.AddScoped<IQueryBus, InMemoryQueryBus>();

var app = builder.Build();

app.MapGrpcService<ParkingService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();

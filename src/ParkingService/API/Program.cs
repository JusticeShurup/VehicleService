using API.Services;
using ProtosContract;
using Infrastructure;
using Infrastructure.Bus.Query;
using System.Reflection;
using Infrastructure.Bus.Command;
using Application.Base.Command;
using Application.Base.Query;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddGrpc().AddJsonTranscoding();
builder.Services.AddGrpcSwagger();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new Microsoft.OpenApi.Models.OpenApiInfo { Title = "gRPC Parking", Version = "v1" });
});

builder.Services.AddGrpcClient<Vehicles.VehiclesClient>(options =>
{
    options.Address = new Uri("https://localhost:7154");
});
builder.Services.AddInfrastructure(configuration);
builder.Services.AddCommandHandlers(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<ICommandBus, InMemoryCommandBus>();
builder.Services.AddQueryHandlers(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<IQueryBus, InMemoryQueryBus>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
});

app.MapGrpcService<ParkingService>();

app.Run();

using Infrastructure;
using API.Services;
using ProtosContract;
using Application.Base.Command;
using Infrastructure.Bus.Command;
using Application.Base.Query;
using Infrastructure.Bus.Query;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddGrpc().AddJsonTranscoding();
builder.Services.AddGrpcSwagger();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new Microsoft.OpenApi.Models.OpenApiInfo { Title = "gRPC Parking", Version = "v1" });

    var filePath = Path.Combine(System.AppContext.BaseDirectory, "Server.xml");
    c.IncludeXmlComments(filePath);
    c.IncludeGrpcXmlComments(filePath, includeControllerXmlComments: true);
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
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();

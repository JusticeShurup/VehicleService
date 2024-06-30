using Infrastructure;
using ParkingService.Services;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructure(configuration);

var app = builder.Build();

app.MapGrpcService<ParkingService.Services.ParkingService>();
app.MapGet("/", () => "Hello!");

app.Run();

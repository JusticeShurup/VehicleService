using API.Services;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddInfrastructure(configuration);

var app = builder.Build();

app.UseRouting();

app.MapGrpcService<VehiclesService>();
app.MapControllers();

app.Run();

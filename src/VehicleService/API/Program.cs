using API.Services;
using Infrastructure;
using ProtosContract;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddGrpc();
builder.Services.AddGrpcClient<Parking.ParkingClient>(options =>
{
    options.Address = new Uri("https://localhost:7076");
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddInfrastructure(configuration);

var app = builder.Build();

app.UseRouting();

app.MapGrpcService<VehiclesService>();
app.MapControllers();

app.Run();

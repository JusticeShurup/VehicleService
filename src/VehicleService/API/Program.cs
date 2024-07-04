using API.Services;
using Application.Base.Command;
using Application.Base.Query;
using Infrastructure;
using Infrastructure.Bus.Command;
using Infrastructure.Bus.Query;
using ProtosContract;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;


builder.Services.AddGrpc().AddJsonTranscoding();
builder.Services.AddGrpcSwagger();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new Microsoft.OpenApi.Models.OpenApiInfo { Title = "gRPC Parking", Version = "v1" });
});
builder.Services.AddGrpcClient<Parking.ParkingClient>(options =>
{
    options.Address = new Uri("https://localhost:7076");
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddInfrastructure(configuration);
builder.Services.AddCommandHandlers(typeof(Command).Assembly);
builder.Services.AddScoped<ICommandBus, InMemoryCommandBus>();
builder.Services.AddQueryHandlers(typeof(Query).Assembly);
builder.Services.AddScoped<IQueryBus, InMemoryQueryBus>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
});


app.UseRouting();

app.MapGrpcService<VehiclesService>();
app.MapControllers();

app.Run();

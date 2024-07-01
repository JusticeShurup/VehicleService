using Domain;
using Domain.Interface;
using Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class VehicleContext : DbContext, IUnitOfWork
{
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<Engine> Engines { get; set; }

    public VehicleContext() { }

    public VehicleContext(DbContextOptions options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Server=localhost;DataBase=postgres;Port=5432;User Id=postgres;Password=super");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new EngineConfiguration());
        modelBuilder.ApplyConfiguration(new VehicleConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}

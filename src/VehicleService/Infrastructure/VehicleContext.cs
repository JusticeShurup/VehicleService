using Domain;
using Domain.Interface;
using Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class VehicleContext : DbContext, IUnitOfWork
{
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<Engine> Engines { get; set; }

    public VehicleContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new VehicleConfiguration());
        modelBuilder.ApplyConfiguration(new EngineConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}

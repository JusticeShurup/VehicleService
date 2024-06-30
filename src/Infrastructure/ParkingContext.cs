using Domain;
using Domain.Interface;
using Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class ParkingContext : DbContext, IUnitOfWork
{
    public DbSet<Parking> Parkings { get; set; }
    public DbSet<ParkingPlace> ParkingPlaces { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }

    public ParkingContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ParkingPlaceConfiguration());
        modelBuilder.ApplyConfiguration(new VehicleConfiguration());
        modelBuilder.ApplyConfiguration(new ParkingConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}

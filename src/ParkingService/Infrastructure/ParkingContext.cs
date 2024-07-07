using Application.Interfaces;
using Domain;
using Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class ParkingContext : DbContext, IUnitOfWork
{
    public DbSet<ParkingPlace> ParkingPlaces { get; set; }
    public DbSet<Parking> Parkings { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }

    public ParkingContext() : base() { }

    public ParkingContext(DbContextOptions options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ParkingConfiguration());
        modelBuilder.ApplyConfiguration(new ParkingPlaceConfiguration());
        modelBuilder.ApplyConfiguration(new SubscriptionConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}

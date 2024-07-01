using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class VehicleConfiguration
        : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(p => p.Name)
                .IsRequired();

            builder.Property(p => p.MaxSpeed)
                .IsRequired();

            builder.Property(p => p.ParkingPlaceId)
                .IsRequired();

        }
    }
}

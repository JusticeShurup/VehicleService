using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


            builder.HasOne(p => p.ParkingPlace)
                .WithOne(p => p.Vehicle)
                .HasForeignKey<ParkingPlace>(p => p.VehicleId);

        }
    }
}

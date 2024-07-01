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
    public class ParkingPlaceConfiguration
        : IEntityTypeConfiguration<ParkingPlace>
    {
        public void Configure(EntityTypeBuilder<ParkingPlace> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Floor)
                .IsRequired();

            builder.Property(x => x.IsWithElectricityCharge)
                .IsRequired();

        }
    }
}

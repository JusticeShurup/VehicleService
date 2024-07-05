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
    public class AbonementConfiguration
        : IEntityTypeConfiguration<Abonement>
    {
        public void Configure(EntityTypeBuilder<Abonement> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(p => p.ParkingPlace)
                .WithMany();
        }
    }
}

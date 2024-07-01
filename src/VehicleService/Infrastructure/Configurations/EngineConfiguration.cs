using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class EngineConfiguration
        : IEntityTypeConfiguration<Engine>
    {
        public void Configure(EntityTypeBuilder<Engine> builder)
        {
            builder.Property(p => p.Vehicle)
                .IsRequired();

            builder.Property(p => p.EngineType)
                .IsRequired()
                .HasConversion<string>();
        }
    }
}

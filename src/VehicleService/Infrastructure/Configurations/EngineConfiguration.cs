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
            builder.HasKey(x => x.Id);


            builder.Property(p => p.EngineType)
                .IsRequired()
                .HasConversion<string>();

            builder.HasOne(p => p.Vehicle)
                .WithOne(p => p.Engine)
                .HasForeignKey<Vehicle>("EngineId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

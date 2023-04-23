using Gymby.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Gymby.Persistence.EntityTypeConfigurations
{
    public class MeasurementConfiguration : IEntityTypeConfiguration<Measurement>
    {
        public void Configure(EntityTypeBuilder<Measurement> builder)
        {
            builder.ToTable("Measurement");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id)
                .IsRequired();

            builder.Property(m => m.UserId)
                .IsRequired();

            builder.Property(m => m.Date)
                .IsRequired();

            builder.Property(m => m.Type)
                .IsRequired();

            builder.Property(m => m.Value)
                .IsRequired();

            builder.Property(m => m.Unit)
                .IsRequired();
        }
    }
}

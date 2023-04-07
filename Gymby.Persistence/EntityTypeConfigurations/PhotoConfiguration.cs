using Gymby.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gymby.Persistence.EntityTypeConfigurations;
public class PhotoConfiguration : IEntityTypeConfiguration<Photo>
{
    public void Configure(EntityTypeBuilder<Photo> builder)
    {
        builder.ToTable("Photo");

        builder.Property(p => p.Id)
            .UseHiLo("photo_hilo")
            .IsRequired();

        builder.Property(p => p.UserId)
             .IsRequired();

        builder.Property(p => p.IsMeasurement)
            .IsRequired();

        builder.Property(p => p.MeasurementDate);

        builder.Property(p => p.CreationDate)
            .IsRequired();
    }
}

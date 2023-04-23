using Gymby.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gymby.Persistence.EntityTypeConfigurations;
public class ApproachConfiguration : IEntityTypeConfiguration<Approach>
{
    public void Configure(EntityTypeBuilder<Approach> builder)
    {
        builder.ToTable("Approach");

        builder.HasKey(da => da.Id);

        builder.Property(da => da.Id)
            .IsRequired();

        builder.Property(a => a.Repeats)
            .IsRequired();

        builder.Property(a => a.Weight)
            .IsRequired();

        builder.Property(a => a.CreationDate)
            .IsRequired();

        builder.HasOne(a => a.Exercise)
            .WithMany(e => e.Approaches)
            .HasForeignKey(a => a.ExerciseId);
    }
}

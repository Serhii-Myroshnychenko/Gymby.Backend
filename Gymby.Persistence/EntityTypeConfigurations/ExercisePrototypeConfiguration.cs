using Gymby.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gymby.Persistence.EntityTypeConfigurations;
public class ExercisePrototypeConfiguration : IEntityTypeConfiguration<ExercisePrototype>
{
    public void Configure(EntityTypeBuilder<ExercisePrototype> builder)
    {
        builder.ToTable("ExercisePrototype");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .IsRequired();

        builder.Property(ep => ep.Name)
            .IsRequired();

        builder.Property(ep => ep.Description)
            .IsRequired();

        builder.Property(ep => ep.Category)
            .IsRequired();

        builder.HasMany(ep => ep.Exercises)
            .WithOne(e => e.ExercisePrototype)
            .HasForeignKey(e => e.ExercisePrototypeId);
          
    }
}

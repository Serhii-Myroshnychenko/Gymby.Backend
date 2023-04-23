using Gymby.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gymby.Persistence.EntityTypeConfigurations;

public class ExerciseConfiguration : IEntityTypeConfiguration<Exercise>
{
    public void Configure(EntityTypeBuilder<Exercise> builder)
    {
        builder.ToTable("Exercise");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .IsRequired();

        builder.Property(e => e.ExercisePrototypeId)
            .IsRequired();

        builder.Property(e => e.Name)
            .HasMaxLength(150)
            .IsRequired();

        builder.HasOne(e => e.DiaryDay)
            .WithMany(dd => dd.Exercises)
            .HasForeignKey(e => e.DiaryDayId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(e => e.ExercisePrototype)
            .WithMany(ep => ep.Exercises)
            .HasForeignKey(e => e.ExercisePrototypeId);

        builder.HasOne(e => e.ProgramDay)
            .WithMany(pd => pd.Exercises)
            .HasForeignKey(e => e.ProgramDayId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(e => e.Approaches)
            .WithOne(a => a.Exercise)
            .HasForeignKey(a => a.ExerciseId);
    }
}

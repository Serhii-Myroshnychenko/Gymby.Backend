using Gymby.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gymby.Persistence.EntityTypeConfigurations;

public class ProgramDayConfiguration : IEntityTypeConfiguration<ProgramDay>
{
    public void Configure(EntityTypeBuilder<ProgramDay> builder)
    {
        builder.ToTable("ProgramDay");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .IsRequired();

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasOne(e => e.Program)
            .WithMany(e => e.ProgramDays)
            .HasForeignKey(e => e.ProgramId)
            .IsRequired();

        builder.HasMany(e => e.Exercises)
            .WithOne(e => e.ProgramDay)
            .HasForeignKey(e => e.ProgramDayId);

        builder.HasMany(e => e.DiaryDays)
            .WithOne(e => e.ProgramDay)
            .HasForeignKey(e => e.ProgramDayId);
    }
}

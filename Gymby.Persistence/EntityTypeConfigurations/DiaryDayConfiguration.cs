using Gymby.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gymby.Persistence.EntityTypeConfigurations;
public class DiaryDayConfiguration : IEntityTypeConfiguration<DiaryDay>
{
    public void Configure(EntityTypeBuilder<DiaryDay> builder)
    {
        builder.ToTable("DiaryDay");

        builder.HasKey(dd => dd.Id);

        builder.Property(dd => dd.Id)
            .IsRequired();

        builder.Property(dd => dd.DiaryId)
            .IsRequired();

        builder.Property(dd => dd.Date)
            .IsRequired();

        builder.HasOne(dd => dd.Diary)
            .WithMany(d => d.DiaryDays)
            .HasForeignKey(dd => dd.DiaryId);

        builder.HasOne(dd => dd.ProgramDay)
            .WithMany(pd => pd.DiaryDays)
            .HasForeignKey(dd => dd.ProgramDayId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(dd => dd.Exercises)
            .WithOne(e => e.DiaryDay)
            .HasForeignKey(e => e.DiaryDayId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}

using Gymby.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gymby.Persistence.EntityTypeConfigurations;
public class DiaryConfiguration : IEntityTypeConfiguration<Diary>
{
    public void Configure(EntityTypeBuilder<Diary> builder)
    {
        builder.ToTable("Diary");

        builder.HasKey(d => d.Id);

        builder.Property(d => d.Id)
            .IsRequired();

        builder.Property(diary => diary.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(diary => diary.CreationDate)
            .IsRequired();

        builder.HasMany(diary => diary.DiaryAccesses)
            .WithOne(diaryAccess => diaryAccess.Diary)
            .HasForeignKey(diaryAccess => diaryAccess.DiaryId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(diary => diary.DiaryDays)
            .WithOne(diaryDay => diaryDay.Diary)
            .HasForeignKey(diaryDay => diaryDay.DiaryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

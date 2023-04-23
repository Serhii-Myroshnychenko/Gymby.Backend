using Gymby.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gymby.Persistence.EntityTypeConfigurations
{
    public class DiaryAccessConfiguration : IEntityTypeConfiguration<DiaryAccess>
    {
        public void Configure(EntityTypeBuilder<DiaryAccess> builder)
        {
            builder.ToTable("DiaryAccess");

            builder.HasKey(da => da.Id);

            builder.Property(da => da.Id)
                .IsRequired();

            builder.Property(m => m.UserId)
                .IsRequired();

            builder.Property(m => m.DiaryId)
                .IsRequired();

            builder.Property(m => m.Type)
                .IsRequired();

            builder.HasOne(diaryAccess => diaryAccess.Diary)
                .WithMany(diary => diary.DiaryAccesses)
                .HasForeignKey(diaryAccess => diaryAccess.DiaryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

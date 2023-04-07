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

            builder.Property(m => m.Id)
                .UseHiLo("DiaryAccess_hilo")
                .IsRequired();

            builder.Property(m => m.UserId)
                .IsRequired();

            builder.Property(m => m.DiaryId)
                .IsRequired();

            builder.Property(m => m.Type)
                .IsRequired();
        }
    }
}

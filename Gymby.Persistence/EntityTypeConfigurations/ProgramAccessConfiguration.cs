using Gymby.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gymby.Persistence.EntityTypeConfigurations;
public class ProgramAccessConfiguration : IEntityTypeConfiguration<ProgramAccess>
{
    public void Configure(EntityTypeBuilder<ProgramAccess> builder)
    {
        builder.ToTable("ProgramAccess");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .IsRequired();

        builder.HasOne(e => e.Program)
            .WithMany(e => e.ProgramAccesses)
            .HasForeignKey(e => e.ProgramId)
            .IsRequired();

        builder.HasIndex(e => new { e.UserId, e.ProgramId }).IsUnique();

        builder.Property(e => e.Type)
            .IsRequired();

        builder.Property(e => e.IsFavorite)
            .IsRequired();
    }
}

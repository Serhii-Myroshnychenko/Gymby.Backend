using Gymby.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gymby.Persistence.EntityTypeConfigurations;
public class ProgramConfiguration : IEntityTypeConfiguration<Program>
{
    public void Configure(EntityTypeBuilder<Program> builder)
    {
        builder.ToTable("Program");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .IsRequired();

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(e => e.IsPublic)
            .IsRequired();

        builder.Property(e => e.Description)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(e => e.Level)
            .IsRequired();

        builder.Property(e => e.Type)
            .IsRequired();

        builder.HasMany(e => e.ProgramAccesses)
            .WithOne(e => e.Program)
            .HasForeignKey(e => e.ProgramId);

        builder.HasMany(e => e.ProgramDays)
            .WithOne(e => e.Program)
            .HasForeignKey(e => e.ProgramId);
    }
}

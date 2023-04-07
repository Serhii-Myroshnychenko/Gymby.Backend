using Gymby.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gymby.Persistence.EntityTypeConfigurations;
public class ProfileConfiguration : IEntityTypeConfiguration<Profile>
{
    public void Configure(EntityTypeBuilder<Profile> builder)
    {
        builder.ToTable("Profile");

        builder.Property(p => p.Id)
            .UseHiLo("profile_hilo")
            .IsRequired();

        builder.Property(p => p.UserId)
            .IsRequired();

        builder.Property(p => p.FirstName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(p => p.LastName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.Description)
            .HasMaxLength(150);

        builder.Property(p => p.PhotoAvatarPath)
            .HasMaxLength(50);

        builder.Property(p => p.InstagramUrl)
            .HasMaxLength(255);

        builder.Property(p => p.FacebookUrl)
            .HasMaxLength(255);

        builder.Property(p => p.TelegramUsername)
            .HasMaxLength(255);

        builder.Property(p => p.Role)
            .IsRequired();
    }
}

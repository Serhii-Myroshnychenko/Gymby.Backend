using Gymby.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gymby.Persistence.EntityTypeConfigurations;
public class ProfileConfiguration : IEntityTypeConfiguration<Profile>
{
    public void Configure(EntityTypeBuilder<Profile> builder)
    {
        builder.ToTable("Profile");

        builder.HasKey(pr => pr.Id);

        builder.Property(pr => pr.Id)
            .IsRequired();

        builder.Property(p => p.UserId)
            .IsRequired();

        builder.Property(p => p.FirstName)
            .HasMaxLength(50);

        builder.Property(p => p.LastName)
            .HasMaxLength(100);

        builder.Property(p => p.Description);

        builder.Property(p => p.PhotoAvatarPath)
            .HasMaxLength(50);

        builder.Property(p => p.InstagramUrl)
            .HasMaxLength(255);

        builder.Property(p => p.FacebookUrl)
            .HasMaxLength(255);

        builder.Property(p => p.TelegramUsername)
            .HasMaxLength(255);

        builder.Property(p => p.Email)
            .IsRequired();

        builder.Property(p => p.Username)
            .IsRequired();
    }
}

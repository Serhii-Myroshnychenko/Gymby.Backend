using Gymby.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gymby.Persistence.EntityTypeConfigurations;
public class FriendConfiguration : IEntityTypeConfiguration<Friend>
{
    public void Configure(EntityTypeBuilder<Friend> builder)
    {
        builder.ToTable("Friend");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .IsRequired();

        builder.Property(m => m.SenderId)
            .IsRequired();

        builder.Property(m => m.ReceiverId)
            .IsRequired();

        builder.Property(m => m.Status)
            .IsRequired();
    }
}

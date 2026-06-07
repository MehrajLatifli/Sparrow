using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sparrow.Domain.Entities.Models;

namespace Sparrow.Domain.EntityFrameworkConfigurations.Music_DBContext
{
    public sealed class PlaylistUserConfiguration : IEntityTypeConfiguration<PlaylistUser>
    {
        public void Configure(EntityTypeBuilder<PlaylistUser> builder)
        {
            builder.HasKey(x => x.Id)
                .HasName("PK__Playlist__3214EC0725AF26E3");

            builder.Property(x => x.Id)
                .HasDefaultValueSql("(newid())");

            builder.HasOne(x => x.PlaylistId_forPlaylistUserNavigation)
                .WithMany(x => x.PlaylistUsers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PlaylistId_forPlaylistUser");
        }
    }
}

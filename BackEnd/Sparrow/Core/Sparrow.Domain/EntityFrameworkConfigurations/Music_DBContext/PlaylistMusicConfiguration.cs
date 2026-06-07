using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sparrow.Domain.Entities.Models;

namespace Sparrow.Domain.EntityFrameworkConfigurations.Music_DBContext
{
    public sealed class PlaylistMusicConfiguration : IEntityTypeConfiguration<PlaylistMusic>
    {
        public void Configure(EntityTypeBuilder<PlaylistMusic> builder)
        {
            builder.HasKey(x => x.Id)
                .HasName("PK__Playlist__3214EC07F614751D");

            builder.Property(x => x.Id)
                .HasDefaultValueSql("(newid())");

            builder.HasOne(x => x.MusicId_forPlaylistMusicNavigation)
                .WithMany(x => x.PlaylistMusics)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MusicId_forPlaylistMusic");

            builder.HasOne(x => x.PlaylistId_forPlaylistMusicNavigation)
                .WithMany(x => x.PlaylistMusics)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PlaylistId_forPlaylistMusic");
        }
    }
}

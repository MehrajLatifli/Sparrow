using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sparrow.Domain.Entities.Models;

namespace Sparrow.Domain.EntityFrameworkConfigurations.Music_DBContext
{
    public sealed class MusicAlbumConfiguration : IEntityTypeConfiguration<MusicAlbum>
    {
        public void Configure(EntityTypeBuilder<MusicAlbum> builder)
        {
            builder.HasKey(x => x.Id)
                .HasName("PK__MusicAlb__3214EC07659FBF02");

            builder.Property(x => x.Id)
                .HasDefaultValueSql("(newid())");

            builder.HasOne(x => x.AlbumId_forMusicAlbumNavigation)
                .WithMany(x => x.MusicAlbums)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AlbumId_forMusicAlbum");

            builder.HasOne(x => x.MusicId_forMusicAlbumNavigation)
                .WithMany(x => x.MusicAlbums)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MusicId_forMusicAlbum");
        }
    }
}

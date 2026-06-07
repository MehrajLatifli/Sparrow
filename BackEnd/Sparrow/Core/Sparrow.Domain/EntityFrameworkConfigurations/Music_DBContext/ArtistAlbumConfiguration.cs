using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sparrow.Domain.Entities.Models;

namespace Sparrow.Domain.EntityFrameworkConfigurations.Music_DBContext
{
    public sealed class ArtistAlbumConfiguration : IEntityTypeConfiguration<ArtistAlbum>
    {
        public void Configure(EntityTypeBuilder<ArtistAlbum> builder)
        {
            builder.HasKey(x => x.Id)
                .HasName("PK__ArtistAl__3214EC07D14339E2");

            builder.Property(x => x.Id)
                .HasDefaultValueSql("(newid())");

            builder.HasOne(x => x.AlbumId_forArtistAlbumNavigation)
                .WithMany(x => x.ArtistAlbums)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AlbumId_forArtistAlbum");

            builder.HasOne(x => x.ArtistId_forArtistAlbumNavigation)
                .WithMany(x => x.ArtistAlbums)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ArtistId_forArtistAlbum");
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sparrow.Domain.Entities.Models;

namespace Sparrow.Domain.EntityFrameworkConfigurations.Music_DBContext
{
    public sealed class ArtistConfiguration : IEntityTypeConfiguration<Artist>
    {
        public void Configure(EntityTypeBuilder<Artist> builder)
        {
            builder.HasKey(x => x.Id)
                .HasName("PK__Artist__3214EC076A4BAD4A");

            builder.Property(x => x.Id)
                .HasDefaultValueSql("(newid())");
        }
    }
}

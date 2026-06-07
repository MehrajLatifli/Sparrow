using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sparrow.Domain.Entities.Models;

namespace Sparrow.Domain.EntityFrameworkConfigurations.Music_DBContext
{
    public sealed class MusicConfiguration : IEntityTypeConfiguration<Music>
    {
        public void Configure(EntityTypeBuilder<Music> builder)
        {
            builder.HasKey(x => x.Id)
                .HasName("PK__Music__3214EC0758136C51");

            builder.Property(x => x.Id)
                .HasDefaultValueSql("(newid())");
        }
    }
}

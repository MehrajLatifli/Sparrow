using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sparrow.Domain.Entities.Models;

namespace Sparrow.Domain.EntityFrameworkConfigurations.Music_DBContext
{
    public sealed class RadioConfiguration : IEntityTypeConfiguration<Radio>
    {
        public void Configure(EntityTypeBuilder<Radio> builder)
        {
            builder.HasKey(x => x.Id)
                .HasName("PK__Radio__3214EC07C45937BC");

            builder.Property(x => x.Id)
                .HasDefaultValueSql("(newid())");
        }
    }
}

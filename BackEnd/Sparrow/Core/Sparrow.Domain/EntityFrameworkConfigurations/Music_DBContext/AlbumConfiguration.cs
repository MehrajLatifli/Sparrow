using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sparrow.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sparrow.Domain.EntityFrameworkConfigurations.Music_DBContext
{
    public sealed class AlbumConfiguration : IEntityTypeConfiguration<Album>
    {
        public void Configure(EntityTypeBuilder<Album> builder)
        {
            builder.HasKey(x => x.Id)
                .HasName("PK__Album__3214EC0777CC10F6");

            builder.Property(x => x.Id)
                .HasDefaultValueSql("(newid())");
        }
    }
}

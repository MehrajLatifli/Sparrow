using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sparrow.Domain.Entities.IdentityAuth;

namespace Sparrow.Domain.EntityFrameworkConfigurations.User_DbContext
{
    public class UserPermissionConfiguration : IEntityTypeConfiguration<UserPermission>
    {
        public void Configure(EntityTypeBuilder<UserPermission> entity)
        {
            entity.HasKey(e => e.Id).HasName("PK__UserPerm__3214EC078B242246");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.UserAccess).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.UserAccessDescription).HasDefaultValueSql("(NULL)");
        }
    }
}
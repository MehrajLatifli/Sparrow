using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sparrow.Domain.Entities.IdentityAuth;

namespace Sparrow.Domain.EntityFrameworkConfigurations.User_DbContext
{
    public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> entity)
        {
            entity.HasKey(e => e.Id).HasName("PK__RolePerm__3214EC078218EC11");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.Method).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.MethodDescription).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(NULL)");
        }
    }
}
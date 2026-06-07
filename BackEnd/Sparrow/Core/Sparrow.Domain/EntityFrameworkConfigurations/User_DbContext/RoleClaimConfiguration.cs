using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sparrow.Domain.Entities.IdentityAuth;

namespace Sparrow.Domain.EntityFrameworkConfigurations.User_DbContext
{
    public class RoleClaimConfiguration : IEntityTypeConfiguration<RoleClaim>
    {
        public void Configure(EntityTypeBuilder<RoleClaim> entity)
        {
            entity.HasKey(e => e.Id).HasName("PK__RoleClai__3214EC07DFD12505");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(NULL)");

            entity.HasOne(d => d.Role).WithMany(p => p.RoleClaims).HasConstraintName("FK_RoleId_forRoleClaim");

            entity.HasOne(d => d.RolePermission).WithMany(p => p.RoleClaims).HasConstraintName("FK_RolePermissionId_forRoleClaim");
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sparrow.Domain.Entities.IdentityAuth;

namespace Sparrow.Domain.EntityFrameworkConfigurations.User_DbContext
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> entity)
        {
            entity.HasKey(e => e.Id).HasName("PK__UserRole__3214EC074D9E1501");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(NULL)");

            entity.HasOne(d => d.Role).WithMany(p => p.UserRoles).HasConstraintName("FK_RoleId_forUserRole");

            entity.HasOne(d => d.User).WithMany(p => p.UserRoles).HasConstraintName("FK_UserId_forUserRole");
        }
    }
}
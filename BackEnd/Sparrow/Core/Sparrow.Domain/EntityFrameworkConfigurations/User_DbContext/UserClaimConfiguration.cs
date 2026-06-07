using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sparrow.Domain.Entities.IdentityAuth;

namespace Sparrow.Domain.EntityFrameworkConfigurations.User_DbContext
{
    public class UserClaimConfiguration : IEntityTypeConfiguration<UserClaim>
    {
        public void Configure(EntityTypeBuilder<UserClaim> entity)
        {
            entity.HasKey(e => e.Id).HasName("PK__UserClai__3214EC075AC0C5B1");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(NULL)");

            entity.HasOne(d => d.User).WithMany(p => p.UserClaims).HasConstraintName("FK_UserId_forUserClaim");

            entity.HasOne(d => d.UserPermition).WithMany(p => p.UserClaims).HasConstraintName("FK_UserPermitionId_forUserClaim");
        }
    }
}
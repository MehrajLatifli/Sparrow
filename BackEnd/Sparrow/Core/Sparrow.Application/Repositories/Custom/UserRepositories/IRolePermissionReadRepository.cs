using Sparrow.Application.Repositories.Abstract;
using Sparrow.Domain.Entities.IdentityAuth;

namespace Sparrow.Application.Repositories.Custom.UserRepositories
{
    public interface IRolePermissionReadRepository : IReadRepository<RolePermission>
    {
    }
}

using Sparrow.Application.Repositories.Custom.UserRepositories;
using Sparrow.Domain.Entities.IdentityAuth;
using Sparrow.Persistence.Contexts.UserDbContext;
using Sparrow.Persistence.Repositories.Concrete.User;

namespace Sparrow.Persistence.Repositories.Custom.User
{
    public class RoleClaimWriteRepository : UserWriteRepository<RoleClaim>, IRoleClaimWriteRepository
    {
        public RoleClaimWriteRepository(User_DbContext context) : base(context)
        {

        }
    }
}

using Sparrow.Application.Repositories.Custom.UserRepositories;
using Sparrow.Domain.Entities.IdentityAuth;
using Sparrow.Persistence.Contexts.UserDbContext;
using Sparrow.Persistence.Repositories.Concrete.User;

namespace Sparrow.Persistence.Repositories.Custom.User
{
    public class RoleWriteRepository : UserWriteRepository<Role>, IRoleWriteRepository
    {
        public RoleWriteRepository(User_DbContext context) : base(context)
        {

        }
    }
}

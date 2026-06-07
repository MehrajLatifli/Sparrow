
using Sparrow.Application.Repositories.Custom.UserRepositories;
using Sparrow.Persistence.Contexts.UserDbContext;
using Sparrow.Persistence.Repositories.Concrete.User;

namespace Sparrow.Persistence.Repositories.Custom.User
{
    public class UserReadRepository
        : UserReadRepository<Domain.Entities.IdentityAuth.User>,
          IUserReadRepository
    {
        public UserReadRepository(User_DbContext context)
            : base(context)
        {
        }
    }
}
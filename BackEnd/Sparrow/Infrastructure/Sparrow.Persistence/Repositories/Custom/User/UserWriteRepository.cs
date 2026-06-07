
using Sparrow.Application.Repositories.Custom.UserRepositories;
using Sparrow.Persistence.Contexts.UserDbContext;
using Sparrow.Persistence.Repositories.Concrete.User;

namespace Sparrow.Persistence.Repositories.Custom.User
{
    public class UserWriteRepository
        : UserWriteRepository<Domain.Entities.IdentityAuth.User>,
          IUserWriteRepository
    {
        public UserWriteRepository(User_DbContext context)
            : base(context)
        {
        }
    }
}
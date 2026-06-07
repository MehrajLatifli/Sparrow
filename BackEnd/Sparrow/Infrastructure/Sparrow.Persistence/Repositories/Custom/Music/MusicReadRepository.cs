using Sparrow.Application.Repositories.Custom.MusicRepositories;
using Sparrow.Persistence.Contexts.MusicDbContext;
using Sparrow.Persistence.Repositories.Concrete.Music;

namespace Sparrow.Persistence.Repositories.Custom.Music
{
    public class MusicReadRepository : MusicReadRepository<Domain.Entities.Models.Music>, IMusicReadRepository
    {
        public MusicReadRepository(Music_DbContext context) : base(context)
        {
        }
    }


}

using Sparrow.Application.Repositories.Custom.MusicRepositories;
using Sparrow.Persistence.Contexts.MusicDbContext;
using Sparrow.Persistence.Repositories.Concrete.Music;

namespace Sparrow.Persistence.Repositories.Custom.Music
{
    public class MusicWriteRepository : MusicWriteRepository<Domain.Entities.Models.Music>, IMusicWriteRepository
    {
        public MusicWriteRepository(Music_DbContext context) : base(context)
        {

        }
    }


}

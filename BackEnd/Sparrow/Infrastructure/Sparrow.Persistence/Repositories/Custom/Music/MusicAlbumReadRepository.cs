using Sparrow.Application.Repositories.Custom.MusicRepositories;
using Sparrow.Domain.Entities.Models;
using Sparrow.Persistence.Contexts.MusicDbContext;
using Sparrow.Persistence.Repositories.Concrete.Music;

namespace Sparrow.Persistence.Repositories.Custom.Music
{
    public class MusicAlbumReadRepository : MusicReadRepository<MusicAlbum>, IMusicAlbumReadRepository
    {
        public MusicAlbumReadRepository(Music_DbContext context) : base(context)
        {
        }
    }


}

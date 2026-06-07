using Sparrow.Application.Repositories.Custom.MusicRepositories;
using Sparrow.Domain.Entities.Models;
using Sparrow.Persistence.Contexts.MusicDbContext;
using Sparrow.Persistence.Repositories.Concrete.Music;

namespace Sparrow.Persistence.Repositories.Custom.Music
{
    public class AlbumReadRepository : MusicReadRepository<Album>, IAlbumReadRepository
    {
        public AlbumReadRepository(Music_DbContext context) : base(context)
        {
        }
    }


}

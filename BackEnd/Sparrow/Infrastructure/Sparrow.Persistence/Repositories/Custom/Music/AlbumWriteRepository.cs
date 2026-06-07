using Sparrow.Application.Repositories.Custom.MusicRepositories;
using Sparrow.Domain.Entities.Models;
using Sparrow.Persistence.Contexts.MusicDbContext;
using Sparrow.Persistence.Repositories.Concrete.Music;

namespace Sparrow.Persistence.Repositories.Custom.Music
{
    public class AlbumWriteRepository : MusicWriteRepository<Album>, IAlbumWriteRepository
    {
        public AlbumWriteRepository(Music_DbContext context) : base(context)
        {

        }
    }


}

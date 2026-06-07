using Sparrow.Application.Repositories.Custom.MusicRepositories;
using Sparrow.Domain.Entities.Models;
using Sparrow.Persistence.Contexts.MusicDbContext;
using Sparrow.Persistence.Repositories.Concrete.Music;

namespace Sparrow.Persistence.Repositories.Custom.Music
{
    public class RadioWriteRepository : MusicWriteRepository<Radio>, IRadioWriteRepository
    {
        public RadioWriteRepository(Music_DbContext context) : base(context)
        {

        }
    }


}

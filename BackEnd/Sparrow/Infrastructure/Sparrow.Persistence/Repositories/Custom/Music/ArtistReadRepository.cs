using Sparrow.Application.Repositories.Custom.MusicRepositories;
using Sparrow.Application.Repositories.Custom.UserRepositories;
using Sparrow.Domain.Entities.IdentityAuth;
using Sparrow.Domain.Entities.Models;
using Sparrow.Persistence.Contexts.MusicDbContext;
using Sparrow.Persistence.Contexts.UserDbContext;
using Sparrow.Persistence.Repositories.Concrete.Music;
using Sparrow.Persistence.Repositories.Concrete.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sparrow.Persistence.Repositories.Custom.Music
{
    public class ArtistReadRepository : MusicReadRepository<Artist>, IArtistReadRepository
    {
        public ArtistReadRepository(Music_DbContext context) : base(context)
        {
        }
    }


}

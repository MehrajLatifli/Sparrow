using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sparrow.Application.Cache.RedisCachePatterns.Abstract.Music
{
    public interface IArtistCacheService<T>
    {
        Task<List<T>> GetAllArtists();
        Task SetAllArtists(List<T> items);
        Task ClearAllArtists();
    }
}
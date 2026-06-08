namespace Sparrow.Application.Cache.RedisCachePatterns.Abstract.Music
{
    public interface IArtistAlbumCacheService<T>
    {
        Task<List<T>> GetAllArtistAlbums();
        Task SetAllArtistAlbums(List<T> items);
        Task ClearAllArtistAlbums();
    }
}
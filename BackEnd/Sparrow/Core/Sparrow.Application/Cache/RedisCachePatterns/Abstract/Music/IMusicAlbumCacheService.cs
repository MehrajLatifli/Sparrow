namespace Sparrow.Application.Cache.RedisCachePatterns.Abstract.Music
{
    public interface IMusicAlbumCacheService<T>
    {
        Task<List<T>> GetAllMusicAlbums();
        Task SetAllMusicAlbums(List<T> items);
        Task ClearAllMusicAlbums();
    }
}
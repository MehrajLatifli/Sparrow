namespace Sparrow.Application.Cache.RedisCachePatterns.Abstract.Music
{
    public interface IAlbumCacheService<T>
    {
        Task<List<T>> GetAllAlbums();
        Task SetAllAlbums(List<T> items);
        Task ClearAllAlbums();
    }
}
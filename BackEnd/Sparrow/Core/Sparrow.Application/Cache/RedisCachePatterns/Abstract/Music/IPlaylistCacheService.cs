namespace Sparrow.Application.Cache.RedisCachePatterns.Abstract.Music
{
    public interface IPlaylistCacheService<T>
    {
        Task<List<T>> GetAllPlaylists();
        Task SetAllPlaylists(List<T> items);
        Task ClearAllPlaylists();
    }
}
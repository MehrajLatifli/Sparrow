namespace Sparrow.Application.Cache.RedisCachePatterns.Abstract.Music
{
    public interface IPlaylistMusicCacheService<T>
    {
        Task<List<T>> GetAllPlaylistMusics();
        Task SetAllPlaylistMusics(List<T> items);
        Task ClearAllPlaylistMusics();
    }
}
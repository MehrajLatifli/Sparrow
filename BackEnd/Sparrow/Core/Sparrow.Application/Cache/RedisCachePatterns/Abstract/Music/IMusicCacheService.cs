namespace Sparrow.Application.Cache.RedisCachePatterns.Abstract.Music
{
    public interface IMusicCacheService<T>
    {
        Task<List<T>> GetAllMusics();
        Task SetAllMusics(List<T> items);
        Task ClearAllMusics();
    }
}
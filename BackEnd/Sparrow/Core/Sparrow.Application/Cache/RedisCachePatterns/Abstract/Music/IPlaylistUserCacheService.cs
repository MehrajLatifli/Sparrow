namespace Sparrow.Application.Cache.RedisCachePatterns.Abstract.Music
{
    public interface IPlaylistUserCacheService<T>
    {
        Task<List<T>> GetAllPlaylisUsers();
        Task SetAllPlaylisUsers(List<T> items);
        Task ClearAllPlaylisUsers();
    }
}
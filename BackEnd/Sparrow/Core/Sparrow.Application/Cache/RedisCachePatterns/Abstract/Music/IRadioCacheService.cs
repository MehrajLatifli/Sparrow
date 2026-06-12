namespace Sparrow.Application.Cache.RedisCachePatterns.Abstract.Music
{
    public interface IRadioCacheService<T>
    {
        Task<List<T>> GetAllRadios();
        Task SetAllRadios(List<T> items);
        Task ClearAllRadios();
    }
}
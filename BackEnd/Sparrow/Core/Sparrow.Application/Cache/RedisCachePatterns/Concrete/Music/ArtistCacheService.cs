using Newtonsoft.Json;
using Sparrow.Application.Cache.RedisCachePatterns.Abstract.Music;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Sparrow.Application.Cache.RedisCachePatterns.Concrete.Music
{
    public class ArtistCacheService<T> : IArtistCacheService<T>
    {
        private readonly IDatabase _database;

        private const string CacheKey = "Artist:All";
        private static readonly TimeSpan CacheDuration = TimeSpan.FromSeconds(30);

        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        public ArtistCacheService(IConnectionMultiplexer connectionMultiplexer)
        {
            _database = connectionMultiplexer?.GetDatabase()
                ?? throw new ArgumentNullException(nameof(connectionMultiplexer));
        }


        public async Task<List<T>> GetAllArtists()
        {
            await _semaphore.WaitAsync();
            try
            {
                var cachedData = await _database.StringGetAsync(CacheKey);

                if (!cachedData.HasValue)
                    return new List<T>();

                return JsonConvert.DeserializeObject<List<T>>(cachedData!);
            }
            finally
            {
                _semaphore.Release();
            }
        }


        public async Task SetAllArtists(List<T> items)
        {
            await _semaphore.WaitAsync();
            try
            {
                var json = JsonConvert.SerializeObject(items);

                await _database.StringSetAsync(
                    CacheKey,
                    json,
                    CacheDuration
                );
            }
            finally
            {
                _semaphore.Release();
            }
        }


        public async Task ClearAllArtists()
        {
            await _database.KeyDeleteAsync(CacheKey);
        }
    }
}
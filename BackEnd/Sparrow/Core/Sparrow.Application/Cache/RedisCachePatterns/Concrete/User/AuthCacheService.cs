using Sparrow.Application.Cache.RedisCachePatterns.Abstract.User;
using StackExchange.Redis;

namespace Sparrow.Application.Cache.RedisCachePatterns.Concrete.User
{
    public class AuthCacheService<T> : IAuthCacheService<T>
    {
        private readonly IDatabase _database;

        private static readonly TimeSpan CacheDuration =
            TimeSpan.FromMinutes(1);

        private readonly SemaphoreSlim _semaphore =
            new SemaphoreSlim(1, 1);

        public AuthCacheService(IConnectionMultiplexer connectionMultiplexer)
        {
            _database = connectionMultiplexer?.GetDatabase()
                        ?? throw new ArgumentNullException(nameof(connectionMultiplexer));
        }

        #region ProfileCache

        private static string GenerateProfileKey(string username)
        {
            return $"Profile:{username}";
        }

        public Task<string> GetProfileKey(string username)
        {
            return Task.FromResult(GenerateProfileKey(username));
        }

        public async Task<T?> GetProfile(string username)
        {
            await _semaphore.WaitAsync();

            try
            {
                var key = GenerateProfileKey(username);

                var value = await _database.StringGetAsync(key);

                if (!value.HasValue)
                {
                    return default;
                }

                return Newtonsoft.Json.JsonConvert
                    .DeserializeObject<T>(value!);
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task AddProfile(string username, T item)
        {
            await _semaphore.WaitAsync();

            try
            {
                var key = GenerateProfileKey(username);

                var value = Newtonsoft.Json.JsonConvert
                    .SerializeObject(item);

                await _database.StringSetAsync(
                    key,
                    value,
                    CacheDuration);
            }
            finally
            {
                _semaphore.Release();
            }
        }

        #endregion

        #region UserCache

        private static string GenerateUserKey(string username)
        {
            return $"User:{username}";
        }

        public Task<string> GetUserKey(string username)
        {
            return Task.FromResult(GenerateUserKey(username));
        }

        public async Task<List<T>> GetAllUsers()
        {
            await _semaphore.WaitAsync();

            try
            {
                var users = new List<T>();

                var endpoints = _database.Multiplexer.GetEndPoints();

                if (endpoints.Length == 0)
                {
                    return users;
                }

                var server = _database.Multiplexer.GetServer(endpoints.First());

                var keys = server.Keys(pattern: "User:*");

                foreach (var key in keys)
                {
                    var value = await _database.StringGetAsync(key);

                    if (!value.HasValue)
                    {
                        continue;
                    }

                    var user = Newtonsoft.Json.JsonConvert
                        .DeserializeObject<T>(value!);

                    if (user is not null)
                    {
                        users.Add(user);
                    }
                }

                return users;
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task<T?> GetUser(string username)
        {
            await _semaphore.WaitAsync();

            try
            {
                var key = GenerateUserKey(username);

                var value = await _database.StringGetAsync(key);

                if (!value.HasValue)
                {
                    return default;
                }

                return Newtonsoft.Json.JsonConvert
                    .DeserializeObject<T>(value!);
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task AddUser(string username, T item)
        {
            await _semaphore.WaitAsync();

            try
            {
                var key = GenerateUserKey(username);

                var value = Newtonsoft.Json.JsonConvert
                    .SerializeObject(item);

                await _database.StringSetAsync(
                    key,
                    value,
                    CacheDuration);
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task UpdateUser(string username, T item)
        {
            await _semaphore.WaitAsync();

            try
            {
                var key = GenerateUserKey(username);

                var value = Newtonsoft.Json.JsonConvert
                    .SerializeObject(item);

                await _database.StringSetAsync(
                    key,
                    value,
                    CacheDuration);
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task DeleteUser(string username)
        {
            await _semaphore.WaitAsync();

            try
            {
                var key = GenerateUserKey(username);

                await _database.KeyDeleteAsync(key);
            }
            finally
            {
                _semaphore.Release();
            }
        }

        #endregion
    }
}
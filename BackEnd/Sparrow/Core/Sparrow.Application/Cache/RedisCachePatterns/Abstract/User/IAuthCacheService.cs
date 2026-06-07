using System;
using System.Collections.Generic;
using System.Text;

namespace Sparrow.Application.Cache.RedisCachePatterns.Abstract.User
{
    public interface IAuthCacheService<T>
    {
        public Task<string> GetProfileKey(string username);
        public Task<T> GetProfile(string username);
        public Task AddProfile(string username, T item);
        public Task<string> GetUserKey(string username);
        public Task<List<T>> GetAllUsers();
        public Task<T> GetUser(string username);
        public Task AddUser(string username, T item);
        public Task UpdateUser(string username, T item);
        public Task DeleteUser(string username);
    }
}

using Sparrow.Domain.Entities.Base;

namespace Sparrow.Application.Repositories.Abstract
{
    public interface IWriteRepository<T> : IRepository<T> where T : BaseEntity
    {
        Task<bool> AddAsync(T entity);

        Task<bool> AddRangeAsync(List<T> entities);

        bool Remove(T entity);

        bool RemoveRange(List<T> entities);

        Task<bool> RemoveByIdAsync(string id);

        bool Update(T entity);

        Task<int> SaveAsync();
    }
}

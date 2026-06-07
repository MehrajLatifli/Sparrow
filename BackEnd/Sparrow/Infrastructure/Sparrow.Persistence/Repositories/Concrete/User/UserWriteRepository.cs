using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Sparrow.Application.Repositories.Abstract;
using Sparrow.Domain.Entities.Base;
using Sparrow.Persistence.Contexts.UserDbContext;

namespace Sparrow.Persistence.Repositories.Concrete.User
{
    public class UserWriteRepository<T> : IWriteRepository<T> where T : BaseEntity
    {

        private readonly User_DbContext _context;

        public UserWriteRepository(User_DbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public async Task<bool> AddAsync(T entity)
        {

            EntityEntry<T> entityEntry = await Table.AddAsync(entity);
            return entityEntry.State == EntityState.Added;


        }

        public async Task<bool> AddRangeAsync(List<T> entities)
        {
            await Table.AddRangeAsync(entities);

            return true;
        }

        public bool Remove(T entity)
        {
            EntityEntry<T> entityEntry = Table.Remove(entity);

            return entityEntry.State == EntityState.Deleted;
        }

        public async Task<bool> RemoveByIdAsync(Guid id)
        {
            T model = await Table.FirstOrDefaultAsync(data => data.Id == id);

            return Remove(model);
        }

        public bool RemoveRange(List<T> entities)
        {
            Table.RemoveRange(entities);

            return true;
        }


        public bool Update(T entity)
        {
            EntityEntry<T> entityEntry = Table.Update(entity);

            return entityEntry.State == EntityState.Modified;
        }


        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public Task<bool> RemoveByIdAsync(string id)
        {
            Table.RemoveRange(Table.Where(data => data.Id.ToString() == id));

            return Task.FromResult(true);
        }
    }
}

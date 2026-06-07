using Microsoft.EntityFrameworkCore;

namespace Sparrow.Application.Repositories.Abstract
{
    public interface IRepository<T> where T : class
    {
        DbSet<T> Table { get; }
    }
}

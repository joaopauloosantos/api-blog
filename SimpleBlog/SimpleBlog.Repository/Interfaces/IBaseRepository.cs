using Microsoft.EntityFrameworkCore.Storage;

namespace SimpleBlog.Repository.Interfaces
{
    public interface IBaseRepository<T> : IDisposable where T : class
    {
        Task<T> AddAsync(T entity);
        Task<T?> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        void Update(T entity);
        void Delete(T entity);

        Task<int> SaveChangesAsync();
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task CommitTransactionAsync(IDbContextTransaction transaction);
        Task RollbackTransactionAsync(IDbContextTransaction transaction);
    }
}

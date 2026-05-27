using System.Linq.Expressions;

namespace HomeGrown.Core.Domain.Interfaces;

/// <summary>
/// Generic base repository — provides common CRUD operations for any entity.
/// Concrete repositories extend this with entity-specific queries.
/// </summary>
public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    Task AddAsync(T entity);
    void Update(T entity);
    void Remove(T entity);
}

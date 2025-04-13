using Core.Utils;

namespace Core.Interfaces;

public interface IRepository<TKey,T>
{
    Task<Result<T>> AddAsync(T entity);
    Task<Result<T>> UpdateAsync(T entity);
    Task<Result> DeleteAsync(T entity);
    Task<Result<T?>> GetByIdAsync(TKey id);
    Task<Result<ICollection<T>>> GetAllAsync();
}
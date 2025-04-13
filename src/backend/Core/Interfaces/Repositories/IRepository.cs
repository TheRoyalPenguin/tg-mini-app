namespace Core.Interfaces;

public interface IRepository<TKey,T>
{
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task<T?> GetByIdAsync(TKey id);
    Task<ICollection<T>> GetAllAsync();
}
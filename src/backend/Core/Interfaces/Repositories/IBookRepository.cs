using Core.Models;
using Core.Utils;

namespace Core.Interfaces.Repositories;

public interface IBookRepository
{
    Task<Result<Book>> GetByIdAsync(int id, CancellationToken ct = default);
    Task<Result<IReadOnlyList<Book>>> ListByModuleAsync(int moduleId, CancellationToken ct = default);
    Task<Result> AddAsync(Book book, CancellationToken ct = default);
    Task<Result> UpdateAsync(Book book, CancellationToken ct = default);
    Task<Result> DeleteAsync(int id, CancellationToken ct = default);
}

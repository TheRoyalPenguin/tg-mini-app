using Core.Models;
using Core.Utils;

namespace Core.Interfaces.Services;

public interface IBookService
{
    Task<Result<Book>> GetByIdAsync(int id, CancellationToken ct = default);
    Task<Result<IReadOnlyList<Book>>> ListByModuleAsync(int moduleId, CancellationToken ct = default);
    Task<Result<int>> AddAsync(Book book, CancellationToken ct = default);
    Task<Result> UpdateAsync(Book book, CancellationToken ct = default);
    Task<Result> DeleteAsync(int id, CancellationToken ct = default);
}

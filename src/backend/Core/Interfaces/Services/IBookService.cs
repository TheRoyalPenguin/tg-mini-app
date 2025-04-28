using Core.Models;
using Core.Utils;

namespace Core.Interfaces.Services;

public interface IBookService
{
    Task<Result<Book>> GetByIdAsync(int id, CancellationToken ct = default);
    Task<Result<IReadOnlyList<Book>>> ListByModuleAsync(int moduleId, CancellationToken ct = default);
    Task<Result<int>> AddAsync(CreateBookModel createBookModel, CancellationToken ct = default);
    Task<Result> UpdateAsync(UpdateBookModel book, CancellationToken ct = default);
    Task<Result> DeleteAsync(int id, CancellationToken ct = default);
}

using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Core.Utils;

namespace Application.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _repository;

    public BookService(IBookRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<Book>> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var result = await _repository.GetByIdAsync(id, ct);
        if (!result.IsSuccess)
            return Result<Book>.Failure(result.ErrorMessage)!;

        return Result<Book>.Success(result.Data)!;
    }

    public async Task<Result<IReadOnlyList<Book>>> ListByModuleAsync(int moduleId, CancellationToken ct = default)
    {
        var result = await _repository.ListByModuleAsync(moduleId, ct);
        if (!result.IsSuccess)
            return Result<IReadOnlyList<Book>>.Failure(result.ErrorMessage)!;

        return Result<IReadOnlyList<Book>>.Success(result.Data)!;
    }

    public async Task<Result<int>> AddAsync(Book book, CancellationToken ct = default)
    {
        var result = await _repository.AddAsync(book, ct);
        if (!result.IsSuccess)
            return Result<int>.Failure(result.ErrorMessage);

        return Result<int>.Success(book.Id);
    }

    public async Task<Result> UpdateAsync(Book book, CancellationToken ct = default)
    {
        var result = await _repository.UpdateAsync(book, ct);
        if (!result.IsSuccess)
            return Result.Failure(result.ErrorMessage);

        return Result.Success();
    }

    public async Task<Result> DeleteAsync(int id, CancellationToken ct = default)
    {
        var result = await _repository.DeleteAsync(id, ct);
        if (!result.IsSuccess)
            return Result.Failure(result.ErrorMessage);

        return Result.Success();
    }
}
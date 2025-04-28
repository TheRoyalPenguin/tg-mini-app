using System.Reflection;
using System;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Core.Utils;

namespace Application.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _repository;
    private readonly IStorageService _storage;

    public BookService(IBookRepository repository, IStorageService storage)
    {
        _repository = repository;
        _storage = storage;
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

    public async Task<Result<int>> AddAsync(CreateBookModel cbm, CancellationToken ct = default)
    {
        string contentKey = GetPath(cbm.CourseId, cbm.ModuleId, cbm.ContentName);
        string? coverKey = null;

        await _storage.UploadAsync(cbm.ContentStream, contentKey, ct);
        if (cbm.CoverStream != null && !string.IsNullOrEmpty(cbm.CoverName))
        {
            coverKey = GetPath(cbm.CourseId, cbm.ModuleId, cbm.CoverName);
            await _storage.UploadAsync(cbm.CoverStream, coverKey, ct);
        }

        var book = new Book
        {
            Title = cbm.Title,
            Author = cbm.Author,
            ModuleId = cbm.ModuleId,
            ContentKey = contentKey,
            CoverKey = string.IsNullOrEmpty(coverKey) ? null : coverKey
        };

        var result = await _repository.AddAsync(book, ct);
        if (!result.IsSuccess)
            return Result<int>.Failure(result.ErrorMessage);

        return Result<int>.Success(book.Id);
    }

    public async Task<Result> UpdateAsync(UpdateBookModel ubm, CancellationToken ct = default)
    {
        var getResult = await _repository.GetByIdAsync(ubm.Id, ct);
        if (!getResult.IsSuccess)
            return Result.Failure(getResult.ErrorMessage);

        var book = getResult.Data!;

        if (ubm.NewContentStream != null && !string.IsNullOrEmpty(ubm.NewContentName))
        {
            await _storage.DeleteAsync(book.ContentKey, ct);
            var newContentKey = GetPath(ubm.CourseId, ubm.ModuleId, ubm.NewContentName);
            await _storage.UploadAsync(ubm.NewContentStream, newContentKey, ct);
            book.ContentKey = newContentKey;
        }
        if (ubm.NewCoverStream != null && !string.IsNullOrEmpty(ubm.NewCoverName))
        {
            if (!string.IsNullOrEmpty(book.CoverKey))
                await _storage.DeleteAsync(book.CoverKey!, ct);

            var newCoverKey = GetPath(ubm.CourseId, ubm.ModuleId, ubm.NewCoverName);
            await _storage.UploadAsync(ubm.NewCoverStream, newCoverKey, ct);
            book.CoverKey = newCoverKey;
        }

        book.Title = ubm.Title;
        book.Author = ubm.Author;
        book.ModuleId = ubm.ModuleId;

        var updateResult = await _repository.UpdateAsync(book, ct);
        if (!updateResult.IsSuccess)
            return Result.Failure(updateResult.ErrorMessage);

        return Result.Success();
    }

    public async Task<Result> DeleteAsync(int id, CancellationToken ct = default)
    {
        var getResult = await _repository.GetByIdAsync(id, ct);
        if (!getResult.IsSuccess)
            return Result.Failure(getResult.ErrorMessage);

        var book = getResult.Data!;

        await _storage.DeleteAsync(book.ContentKey, ct);
        if (!string.IsNullOrEmpty(book.CoverKey))
            await _storage.DeleteAsync(book.CoverKey!, ct);

        var deleteResult = await _repository.DeleteAsync(id, ct);
        if (!deleteResult.IsSuccess)
            return Result.Failure(deleteResult.ErrorMessage);

        return Result.Success();
    }

    private static string GetPath(int courseId, int moduleId, string fileName)
    {
        return $"courses/{courseId}/modules/{moduleId}/books/{Guid.NewGuid()}_{fileName}";
    }
}

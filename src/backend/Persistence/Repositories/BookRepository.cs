using AutoMapper;
using Core.Interfaces.Repositories;
using Core.Models;
using Core.Utils;
using Microsoft.EntityFrameworkCore;
using Persistence.Entities;

namespace Persistence.Repositories;

public class BookRepository : IBookRepository
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;

    public BookRepository(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<Result<Book>> GetByIdAsync(int id, CancellationToken ct = default)
    {
        try
        {
            var entity = await _db.Books
                                  .Include(b => b.Module)
                                  .FirstOrDefaultAsync(b => b.Id == id, ct);

            return Result<Book>.Success(_mapper.Map<Book>(entity))!;
        }
        catch (Exception ex)
        {
            return Result<Book>.Failure($"Failed to get book: {ex.Message}")!;
        }
    }

    public async Task<Result<IReadOnlyList<Book>>> ListByModuleAsync(int moduleId, CancellationToken ct = default)
    {
        try
        {
            var entities = await _db.Books
                                     .Where(mb => mb.ModuleId == moduleId)
                                     .ToListAsync(ct);

            return Result<IReadOnlyList<Book>>.Success(_mapper.Map<List<Book>>(entities))!;
        }
        catch (Exception ex)
        {
            return Result<IReadOnlyList<Book>>.Failure($"Failed to list books by moduleId: {ex.Message}")!;
        }
    }

    public async Task<Result> AddAsync(Book book, CancellationToken ct = default)
    {
        try
        {
            var entity = _mapper.Map<BookEntity>(book);
            _db.Books.Add(entity);
            await _db.SaveChangesAsync(ct);
            book.Id = entity.Id;

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure($"Failed to add book: {ex.Message}");
        }
    }

    public async Task<Result> UpdateAsync(Book book, CancellationToken ct = default)
    {
        try
        {
            var entity = await _db.Books
                                  .FirstOrDefaultAsync(b => b.Id == book.Id, ct)
                      ?? throw new KeyNotFoundException("Book not found");

            _mapper.Map(book, entity);
            await _db.SaveChangesAsync(ct);

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure($"Failed to update book: {ex.Message}");
        }
    }

    public async Task<Result> DeleteAsync(int id, CancellationToken ct = default)
    {
        try
        {
            var entity = await _db.Books.FindAsync(id, ct);
            if (entity == null)
                return Result.Failure($"Book with id {id} not found.");

            _db.Books.Remove(entity);
            await _db.SaveChangesAsync(ct);

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure($"Failed to delete book by id: {ex.Message}");
        }
    }
}

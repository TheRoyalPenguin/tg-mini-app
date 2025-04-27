using AutoMapper;
using Core.Interfaces.Repositories;
using Core.Models;
using Core.Utils;
using Microsoft.EntityFrameworkCore;
using Persistence.Entities;

namespace Persistence.Repositories;

public class TestRepository : ITestRepository
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;

    public TestRepository(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<Result<Test>> GetByIdAsync(int id, CancellationToken ct = default)
    {
        try
        {
            var entity = await _db.Tests
                                  .FirstOrDefaultAsync(t => t.Id == id, ct);

            return Result<Test>.Success(_mapper.Map<Test>(entity))!;
        }
        catch (Exception ex)
        {
            return Result<Test>.Failure($"Failed to get test: {ex.Message}")!;
        }
    }

    public async Task<Result<IReadOnlyList<Test>>> ListByModuleAsync(int moduleId, CancellationToken ct = default)
    {
        try
        {
            var entities = await _db.Tests
                                     .Where(t => t.ModuleId == moduleId)
                                     .ToListAsync(ct);

            return Result<IReadOnlyList<Test>>.Success(_mapper.Map<List<Test>>(entities))!;
        }
        catch (Exception ex)
        {
            return Result<IReadOnlyList<Test>>.Failure($"Failed to list tests by moduleId: {ex.Message}")!;
        }
    }

    public async Task<Result> AddAsync(Test test, CancellationToken ct = default)
    {
        try
        {
            var entity = _mapper.Map<TestEntity>(test);
            _db.Tests.Add(entity);
            await _db.SaveChangesAsync(ct);
            test.Id = entity.Id;

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure($"Failed to add test: {ex.Message}");
        }
    }

    public async Task<Result> UpdateAsync(Test test, CancellationToken ct = default)
    {
        try
        {
            var entity = await _db.Tests
                                  .FirstOrDefaultAsync(t => t.Id == test.Id, ct)
                      ?? throw new KeyNotFoundException("Test not found");

            _mapper.Map(test, entity);
            await _db.SaveChangesAsync(ct);

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure($"Failed to update test: {ex.Message}");
        }
    }

    public async Task<Result> DeleteAsync(int id, CancellationToken ct = default)
    {
        try
        {
            var entity = new TestEntity { Id = id };
            _db.Tests.Attach(entity);
            _db.Tests.Remove(entity);
            await _db.SaveChangesAsync(ct);

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure($"Failed to delete test by id: {ex.Message}");
        }
    }
}

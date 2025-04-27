using AutoMapper;
using Core.Interfaces.Repositories;
using Core.Models;
using Core.Utils;
using Microsoft.EntityFrameworkCore;
using Persistence.Entities;

namespace Persistence.Repositories;

public class LongreadRepository : ILongreadRepository
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;

    public LongreadRepository(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<Result<Longread>> GetByIdAsync(int id, CancellationToken ct = default)
    {
        try
        {
            var entity = await _db.Longreads
                      .Include(l => l.Images)
                      .FirstOrDefaultAsync(l => l.Id == id, ct);

            return Result<Longread>.Success(_mapper.Map<Longread>(entity))!;
        }
        catch (Exception ex)
        {
            return Result<Longread>.Failure($"Failed to get longread: {ex.Message}")!;
        }
    }

    public async Task<Result<IReadOnlyList<Longread>>> ListByModuleAsync(int moduleId, CancellationToken ct = default)
    {
        try
        {
            var entities = await _db.Longreads
                         .Where(l => l.ModuleId == moduleId)
                         .Include(l => l.Images)
                         .ToListAsync(ct);

            return Result<IReadOnlyList<Longread>>.Success(_mapper.Map<List<Longread>>(entities));
        }
        catch (Exception ex)
        {
            return Result<IReadOnlyList<Longread>>.Failure($"Failed to get longreads by moduleId: {ex.Message}")!;
        }
    }

    public async Task<Result> AddAsync(Longread longread, CancellationToken ct = default)
    {
        try
        {
            var entity = _mapper.Map<LongreadEntity>(longread);
            _db.Longreads.Add(entity);
            await _db.SaveChangesAsync(ct);
            longread.Id = entity.Id;

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure($"Failed to add longread: {ex.Message}");
        }
    }

    public async Task<Result> UpdateAsync(Longread longread, CancellationToken ct = default)
    {
        try
        {
            var entity = await _db.Longreads
                      .Include(l => l.Images)
                      .FirstOrDefaultAsync(l => l.Id == longread.Id, ct)
            ?? throw new KeyNotFoundException("Longread not found");
            _mapper.Map(longread, entity);
            await _db.SaveChangesAsync(ct);

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure($"Failed to update longread: {ex.Message}");
        }
    }

    public async Task<Result> DeleteAsync(int id, CancellationToken ct = default)
    {
        try
        {
            var entity = new LongreadEntity { Id = id };
            _db.Longreads.Attach(entity);
            _db.Longreads.Remove(entity);
            await _db.SaveChangesAsync(ct);

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure($"Failed to delete longread by id: {ex.Message}");
        }
    }
}

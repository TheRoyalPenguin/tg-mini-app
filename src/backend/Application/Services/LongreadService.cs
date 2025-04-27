using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Core.Utils;

namespace Application.Services;

public class LongreadService : ILongreadService
{
    private readonly ILongreadRepository _repository;

    public LongreadService(ILongreadRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<Longread>> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var longreadResult = await _repository.GetByIdAsync(id, ct);

        if (!longreadResult.IsSuccess)
        {
            return Result<Longread>.Failure(longreadResult.ErrorMessage);
        }

        return Result<Longread>.Success(longreadResult.Data)!;
    }

    public async Task<Result<IReadOnlyList<Longread>>> ListByModuleAsync(int moduleId, CancellationToken ct = default)
    {
        var longreadsResult = await _repository.ListByModuleAsync(moduleId, ct);

        if (!longreadsResult.IsSuccess)
        {
            return Result<IReadOnlyList<Longread>>.Failure(longreadsResult.ErrorMessage);
        }

        return Result<IReadOnlyList<Longread>>.Success(longreadsResult.Data)!;
    }

    public async Task<Result> AddAsync(Longread longread, CancellationToken ct = default)
    {
        var result = await _repository.AddAsync(longread, ct);

        if (!result.IsSuccess)
        {
            return Result.Failure(result.ErrorMessage);
        }

        return Result.Success();
    }

    public async Task<Result> UpdateAsync(Longread longread, CancellationToken ct = default)
    {
        var result = await _repository.UpdateAsync(longread, ct);

        if (!result.IsSuccess)
        {
            return Result.Failure(result.ErrorMessage);
        }

        return Result.Success();
    }

    public async Task<Result> DeleteAsync(int id, CancellationToken ct = default)
    {
        var result = await _repository.DeleteAsync(id, ct);

        if (!result.IsSuccess)
        {
            return Result.Failure(result.ErrorMessage);
        }

        return Result.Success();
    }
}

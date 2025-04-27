using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Core.Utils;

namespace Application.Services;

public class TestService : ITestService
{
    private readonly ITestRepository _repository;

    public TestService(ITestRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<Test>> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var result = await _repository.GetByIdAsync(id, ct);
        if (!result.IsSuccess)
            return Result<Test>.Failure(result.ErrorMessage)!;

        return Result<Test>.Success(result.Data)!;
    }

    public async Task<Result<IReadOnlyList<Test>>> ListByModuleAsync(int moduleId, CancellationToken ct = default)
    {
        var result = await _repository.ListByModuleAsync(moduleId, ct);
        if (!result.IsSuccess)
            return Result<IReadOnlyList<Test>>.Failure(result.ErrorMessage)!;

        return Result<IReadOnlyList<Test>>.Success(result.Data)!;
    }

    public async Task<Result<int>> AddAsync(Test test, CancellationToken ct = default)
    {
        var result = await _repository.AddAsync(test, ct);
        if (!result.IsSuccess)
            return Result<int>.Failure(result.ErrorMessage);

        return Result<int>.Success(test.Id);
    }

    public async Task<Result> UpdateAsync(Test test, CancellationToken ct = default)
    {
        var result = await _repository.UpdateAsync(test, ct);
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

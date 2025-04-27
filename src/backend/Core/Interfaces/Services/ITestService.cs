using Core.Models;
using Core.Utils;

namespace Core.Interfaces.Services;

public interface ITestService
{
    Task<Result<Test>> GetByIdAsync(int id, CancellationToken ct = default);
    Task<Result<IReadOnlyList<Test>>> ListByModuleAsync(int moduleId, CancellationToken ct = default);
    Task<Result<int>> AddAsync(Test test, CancellationToken ct = default);
    Task<Result> UpdateAsync(Test test, CancellationToken ct = default);
    Task<Result> DeleteAsync(int id, CancellationToken ct = default);
}

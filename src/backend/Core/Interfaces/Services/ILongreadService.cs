using Core.Models;
using Core.Utils;

namespace Core.Interfaces.Services;

public interface ILongreadService
{
    Task<Result<Longread>> GetByIdAsync(int id, CancellationToken ct = default);
    Task<Result<IReadOnlyList<Longread>>> ListByModuleAsync(int moduleId, CancellationToken ct = default);
    Task<Result> AddAsync(Longread longread, CancellationToken ct = default);
    Task<Result> UpdateAsync(Longread longread, CancellationToken ct = default);
    Task<Result> DeleteAsync(int id, CancellationToken ct = default);
}

using Core.Models;
using Core.Utils;

namespace Core.Interfaces.Repositories;

public interface IModuleAccessRepository : IRepository<int, ModuleAccess>
{
    Task<Result<ICollection<ModuleAccess>>> GetAllByUserIdAndCourseIdAsync(int userId, int courseId);
    Task<Result<ICollection<ModuleAccess>>> GetAllByCourseIdAsync(int courseId);
    Task<Result<ICollection<ModuleAccess>>> GetAllByModuleIdAsync(int moduleId);
    Task<Result<ICollection<ModuleAccess>>> GetAllByUserIdAsync(int userId);
}
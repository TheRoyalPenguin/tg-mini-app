using Core.Utils;
using Module = Core.Models.Module;

namespace Core.Interfaces.Repositories;

public interface IModuleRepository : IRepository<int, Module>
{
    Task<Result> DeleteAsync(int id);
    Task<Result<ICollection<Module>>> GetAllByCourseIdAsync(int courseId);
    Task<Result<bool>> ExistsAsync(int moduleId);
    Task<Result<bool>> ExistsForCourseAsync(int courseId, int moduleId);
    Task<Result<Module>> GetNextModuleInCourseAsync(int courseId, int currentModuleId);
}
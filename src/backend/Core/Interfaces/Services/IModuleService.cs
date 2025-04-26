using Core.Models;
using Core.Utils;

namespace Core.Interfaces.Services;

public interface IModuleService
{
    Task<Result<Module>> AddModuleAsync(Module module);
    Task<Result<Module>> UpdateModuleAsync(Module module);
    Task<Result> DeleteModuleAsync(int id);
    Task<Result<Module?>> GetModuleByIdAsync(int id);
    Task<Result<ICollection<Module>>> GetModulesByCourseIdAsync(int courseId);
    Task<Result<ICollection<Module>>> GetAllModulesAsync();
    Task<Result<ICollection<ModuleWithAccess>>> GetModulesByCourseIdWithAccessAsync(int courseId, int userId);
}
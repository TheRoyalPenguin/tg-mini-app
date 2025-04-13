using Core.Models;
using Core.Utils;

namespace Core.Interfaces.Services;

public interface IModuleService
{
    Task<Result<Module>> AddModuleAsync(Module module);
    Task<Result<Module>> UpdateModuleAsync(Module module);
    Task<Result> DeleteModuleAsync(Module module);
    Task<Result<Module?>> GetModuleByIdAsync(int id);
    Task<Result<List<Module>>> GetModulesByCourseIdAsync(int courseId);
    Task<Result<List<Module>>> GetAllModulesAsync();
}
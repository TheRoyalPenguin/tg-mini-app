using Core.Models;
using Core.Utils;

namespace Core.Interfaces.Services;

public interface IModuleAccessService
{
        Task<Result<ModuleAccess>> AddModuleAccessAsync(ModuleAccess moduleAccess);
        Task<Result<ICollection<ModuleAccess>>> AddAccessesForEveryModuleForUserAsync(int userId, int courseId);
        
        Task<Result<ModuleAccess>> UpdateModuleAccessAsync(ModuleAccess moduleAccess);
        Task<Result> DeleteModuleAccessAsync(ModuleAccess moduleAccess);
        
        Task<Result<ModuleAccess?>> GetModuleAccessByIdAsync(int id);
        Task<Result<ICollection<ModuleAccess>>> GetModuleAccessesByCourseIdAsync(int courseId);
        Task<Result<ICollection<ModuleAccess>>> GetModuleAccessesByModuleIdAsync(int moduleId);
        Task<Result<ICollection<ModuleAccess>>> GetModuleAccessesByUserIdAsync(int userId);
        Task<Result<ICollection<ModuleAccess>>> GetModuleAccessesForUserByCourseIdAsync(int userId, int courseId);
        Task<Result<ICollection<ModuleAccess>>> GetAllModuleAccessesAsync();
}
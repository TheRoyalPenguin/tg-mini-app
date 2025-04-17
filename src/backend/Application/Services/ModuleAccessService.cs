using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Core.Utils;

namespace Application.Services;

public class ModuleAccessService(IModuleAccessRepository moduleAccessRepository) : IModuleAccessService
{
    public async Task<Result<ModuleAccess>> AddModuleAccessAsync(ModuleAccess moduleAccess)
    {
        var repositoryResult = await moduleAccessRepository.AddAsync(moduleAccess);
        return repositoryResult.IsSuccess
            ? Result<ModuleAccess>.Success(repositoryResult.Data)
            : Result<ModuleAccess>.Failure(repositoryResult.ErrorMessage!)!;
    }

    public async Task<Result<ModuleAccess>> UpdateModuleAccessAsync(ModuleAccess moduleAccess)
    {
        var repositoryResult = await moduleAccessRepository.UpdateAsync(moduleAccess);
        return repositoryResult.IsSuccess
            ? Result<ModuleAccess>.Success(repositoryResult.Data)
            : Result<ModuleAccess>.Failure(repositoryResult.ErrorMessage!)!;
    }

    public async Task<Result> DeleteModuleAccessAsync(ModuleAccess moduleAccess)
    {
        var repositoryResult = await moduleAccessRepository.DeleteAsync(moduleAccess);
        return repositoryResult.IsSuccess
            ? Result.Success()
            : Result.Failure(repositoryResult.ErrorMessage!);
    }

    public async Task<Result<ModuleAccess?>> GetModuleAccessByIdAsync(int id)
    {
        var repositoryResult = await moduleAccessRepository.GetByIdAsync(id);
        return repositoryResult.IsSuccess
            ? Result<ModuleAccess?>.Success(repositoryResult.Data)
            : Result<ModuleAccess?>.Failure(repositoryResult.ErrorMessage!);
    }

    public async Task<Result<ICollection<ModuleAccess>>> GetModuleAccessesByCourseIdAsync(int courseId)
    {
        var repositoryResult = await moduleAccessRepository.GetAllByCourseIdAsync(courseId);
        return repositoryResult.IsSuccess
            ? Result<ICollection<ModuleAccess>>.Success(repositoryResult.Data)
            : Result<ICollection<ModuleAccess>>.Failure(repositoryResult.ErrorMessage!)!;
    }

    public async Task<Result<ICollection<ModuleAccess>>> GetModuleAccessesByModuleIdAsync(int moduleId)
    {
        var repositoryResult = await moduleAccessRepository.GetAllByModuleIdAsync(moduleId);
        return repositoryResult.IsSuccess
            ? Result<ICollection<ModuleAccess>>.Success(repositoryResult.Data)
            : Result<ICollection<ModuleAccess>>.Failure(repositoryResult.ErrorMessage!)!;
    }

    public async Task<Result<ICollection<ModuleAccess>>> GetModuleAccessesByUserIdAsync(int userId)
    {
        var repositoryResult = await moduleAccessRepository.GetAllByUserIdAsync(userId);
        return repositoryResult.IsSuccess
            ? Result<ICollection<ModuleAccess>>.Success(repositoryResult.Data)
            : Result<ICollection<ModuleAccess>>.Failure(repositoryResult.ErrorMessage!)!;
    }

    public async Task<Result<ICollection<ModuleAccess>>> GetModuleAccessesForUserByCourseIdAsync(int userId, int courseId)
    {
        var repositoryResult = await moduleAccessRepository.GetAllByUserIdAndCourseIdAsync(userId, courseId);
        return repositoryResult.IsSuccess
            ? Result<ICollection<ModuleAccess>>.Success(repositoryResult.Data)
            : Result<ICollection<ModuleAccess>>.Failure(repositoryResult.ErrorMessage!)!;
    }

    public async Task<Result<ICollection<ModuleAccess>>> GetAllModuleAccessesAsync()
    {
        var repositoryResult = await moduleAccessRepository.GetAllAsync();
        return repositoryResult.IsSuccess
            ? Result<ICollection<ModuleAccess>>.Success(repositoryResult.Data)
            : Result<ICollection<ModuleAccess>>.Failure(repositoryResult.ErrorMessage!)!;
    }
}
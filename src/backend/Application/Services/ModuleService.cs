using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Core.Utils;

namespace Application.Services;

public class ModuleService(IModuleRepository moduleRepository, IModuleAccessRepository accessRepo) : IModuleService
{
    public async Task<Result<Module>> AddModuleAsync(Module module)
    {
        var repositoryResult = await moduleRepository.AddAsync(module);
        return repositoryResult.IsSuccess 
            ? Result<Module>.Success(repositoryResult.Data)
            : Result<Module>.Failure(repositoryResult.ErrorMessage!)!;
    }

    public async Task<Result<Module>> UpdateModuleAsync(Module module)
    {
        var repositoryResult = await moduleRepository.UpdateAsync(module);
        return repositoryResult.IsSuccess 
            ? Result<Module>.Success(repositoryResult.Data)
            : Result<Module>.Failure(repositoryResult.ErrorMessage!)!;
    }

    public async Task<Result> DeleteModuleAsync(Module module)
    {
        var repositoryResult = await moduleRepository.DeleteAsync(module);
        return repositoryResult.IsSuccess 
            ? Result.Success()
            : Result.Failure(repositoryResult.ErrorMessage!);
    }
    
    public async Task<Result> DeleteModuleAsync(int id)
    {
        var repositoryResult = await moduleRepository.DeleteAsync(id);
        return repositoryResult.IsSuccess 
            ? Result.Success()
            : Result.Failure(repositoryResult.ErrorMessage!);
    }

    public async Task<Result<Module?>> GetModuleByIdAsync(int id)
    {
        var repositoryResult = await moduleRepository.GetByIdAsync(id);
        return repositoryResult.IsSuccess 
            ? Result<Module?>.Success(repositoryResult.Data)
            : Result<Module?>.Failure(repositoryResult.ErrorMessage!);
    }

    public async Task<Result<ICollection<Module>>> GetModulesByCourseIdAsync(int courseId)
    {
        var repositoryResult = await moduleRepository.GetAllByCourseIdAsync(courseId);
        return repositoryResult.IsSuccess 
            ? Result<ICollection<Module>>.Success(repositoryResult.Data)
            : Result<ICollection<Module>>.Failure(repositoryResult.ErrorMessage!)!;
    }

    public async Task<Result<ICollection<ModuleWithAccess>>> GetModulesByCourseIdWithAccessAsync(int courseId, int userId)
    {
        var modulesResult = await moduleRepository.GetAllByCourseIdAsync(courseId);

        if (!modulesResult.IsSuccess)
        {
            return Result<ICollection<ModuleWithAccess>>
                .Failure(modulesResult.ErrorMessage!)!;
        }

        var accessesIds = (await accessRepo.GetAllByUserIdAndCourseIdAsync(userId, courseId))
            .Data
            .Select(a => a.ModuleId)
            .ToHashSet();

        var result = modulesResult.Data
            .Select(m => new ModuleWithAccess
            {
                Id = m.Id,
                Title = m.Title,
                IsAccessed = accessesIds.Contains(m.Id)
            })
            .ToList();

        return Result<ICollection<ModuleWithAccess>>.Success(result);
    }

    public async Task<Result<ICollection<Module>>> GetAllModulesAsync()
    {
        var repositoryResult = await moduleRepository.GetAllAsync();
        return repositoryResult.IsSuccess 
            ? Result<ICollection<Module>>.Success(repositoryResult.Data)
            : Result<ICollection<Module>>.Failure(repositoryResult.ErrorMessage!)!;
    }
}
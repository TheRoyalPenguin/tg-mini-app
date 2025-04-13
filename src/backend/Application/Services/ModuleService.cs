using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Core.Utils;

namespace Application.Services;

public class ModuleService(IModuleRepository moduleRepository) : IModuleService
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

    public async Task<Result<ICollection<Module>>> GetAllModulesAsync()
    {
        var repositoryResult = await moduleRepository.GetAllAsync();
        return repositoryResult.IsSuccess 
            ? Result<ICollection<Module>>.Success(repositoryResult.Data)
            : Result<ICollection<Module>>.Failure(repositoryResult.ErrorMessage!)!;
    }
}
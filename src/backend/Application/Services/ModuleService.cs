using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Core.Utils;

namespace Application.Services;

public class ModuleService(IUnitOfWork uow) : IModuleService
{
    public async Task<Result<Module>> AddModuleAsync(Module module)
    {
        var repositoryResult = await uow.Modules.AddAsync(module);
        
        if (!repositoryResult.IsSuccess)
            return Result<Module>.Failure(repositoryResult.ErrorMessage!)!;

        await uow.SaveChangesAsync();
        
        return Result<Module>.Success(repositoryResult.Data);
    }

    public async Task<Result<Module>> UpdateModuleAsync(Module module)
    {
        var repositoryResult = await uow.Modules.UpdateAsync(module);
        if (!repositoryResult.IsSuccess)
            return Result<Module>.Failure(repositoryResult.ErrorMessage!)!;

        await uow.SaveChangesAsync();
        
        return Result<Module>.Success(repositoryResult.Data);
    }

    public async Task<Result> DeleteModuleAsync(Module module)
    {
        var repositoryResult = await uow.Modules.DeleteAsync(module);
        if (!repositoryResult.IsSuccess)
            return Result.Failure(repositoryResult.ErrorMessage!)!;

        await uow.SaveChangesAsync();
        
        return Result.Success();
    }
    
    public async Task<Result> DeleteModuleAsync(int id)
    {
        var repositoryResult = await uow.Modules.DeleteAsync(id);
        if (!repositoryResult.IsSuccess)
            return Result.Failure(repositoryResult.ErrorMessage!)!;

        await uow.SaveChangesAsync();
        
        return Result.Success();
    }

    public async Task<Result<Module?>> GetModuleByIdAsync(int id)
    {
        var repositoryResult = await uow.Modules.GetByIdAsync(id);
        return repositoryResult.IsSuccess 
            ? Result<Module?>.Success(repositoryResult.Data)
            : Result<Module?>.Failure(repositoryResult.ErrorMessage!);
    }

    public async Task<Result<ICollection<Module>>> GetModulesByCourseIdAsync(int courseId)
    {
        var repositoryResult = await uow.Modules.GetAllByCourseIdAsync(courseId);
        return repositoryResult.IsSuccess 
            ? Result<ICollection<Module>>.Success(repositoryResult.Data)
            : Result<ICollection<Module>>.Failure(repositoryResult.ErrorMessage!)!;
    }

    public async Task<Result<ICollection<Module>>> GetAllModulesAsync()
    {
        var repositoryResult = await uow.Modules.GetAllAsync();
        return repositoryResult.IsSuccess 
            ? Result<ICollection<Module>>.Success(repositoryResult.Data)
            : Result<ICollection<Module>>.Failure(repositoryResult.ErrorMessage!)!;
    }
}
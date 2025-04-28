using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Core.Utils;

namespace Application.Services;

public class ModuleService(IUnitOfWork uow) : IModuleService
{
    public async Task<Result<Module>> AddModuleAsync(Module module)
    {
        await uow.StartTransactionAsync();
        
        var addModuleResult = await uow.Modules.AddAsync(module);
        if (!addModuleResult.IsSuccess)
        {
            await uow.RollbackTransactionAsync();
            
            return Result<Module>.Failure(addModuleResult.ErrorMessage!)!;
        }
        
        var addedModuleModel = addModuleResult.Data;
        var addAccessesResult = await uow.ModuleAccesses.AddAccessForModuleForEveryUsersAsync(addedModuleModel.Id);
        
        await uow.SaveChangesAsync();
        if (!addAccessesResult.IsSuccess)
        {
            await uow.RollbackTransactionAsync();
            
            return Result<Module>.Failure(addAccessesResult.ErrorMessage!)!;
        }
        
        await uow.CommitTransactionAsync();
        
        return Result<Module>.Success(addModuleResult.Data);
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

    public async Task<Result<ICollection<ModuleWithAccess>>> GetModulesByCourseIdWithAccessAsync(int courseId, int userId)
    {
        if (courseId <= 0 || userId <= 0)
            return Result<ICollection<ModuleWithAccess>>.Failure("Invalid input parameters")!;

        var accessResult = await uow.ModuleAccesses.GetAllByUserIdAndCourseIdAsync(userId, courseId);
        
        if (!accessResult.IsSuccess)
            return Result<ICollection<ModuleWithAccess>>.Failure(accessResult.ErrorMessage!)!;
        
        var accesses = accessResult.Data;
        
        if (accesses.Count == 0)
            return Result<ICollection<ModuleWithAccess>>.Failure("Module data not found")!;

        var result = accesses
            .Select(a => new ModuleWithAccess
            {
                Id = a.ModuleId,
                Title = a.Module!.Title,
                IsAccessed = a.IsModuleAvailable
            })
            .ToList();

        return Result<ICollection<ModuleWithAccess>>.Success(result);
    }


    public async Task<Result<ICollection<Module>>> GetAllModulesAsync()
    {
        var repositoryResult = await uow.Modules.GetAllAsync();
        return repositoryResult.IsSuccess 
            ? Result<ICollection<Module>>.Success(repositoryResult.Data)
            : Result<ICollection<Module>>.Failure(repositoryResult.ErrorMessage!)!;
    }
}
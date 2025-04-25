using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Core.Utils;

namespace Application.Services;

public class ModuleAccessService(IUnitOfWork uow) : IModuleAccessService
{
    public async Task<Result<ModuleAccess>> AddModuleAccessAsync(ModuleAccess moduleAccess)
    {
        var repositoryResult = await uow.ModuleAccesses.AddAsync(moduleAccess);

        if (!repositoryResult.IsSuccess) 
            return Result<ModuleAccess>.Failure(repositoryResult.ErrorMessage!)!;
        
        await uow.SaveChangesAsync();
        
        return Result<ModuleAccess>.Success(repositoryResult.Data);
    }

    public async Task<Result<ICollection<ModuleAccess>>> AddAccessesForEveryModuleForUserAsync(int userId, int courseId)
    {
        var repositoryResult = await uow.ModuleAccesses.AddAccessesForEveryModuleForUserAsync(userId, courseId);
            
        if (!repositoryResult.IsSuccess) 
            return Result<ICollection<ModuleAccess>>.Failure(repositoryResult.ErrorMessage!)!; 
        
        await uow.SaveChangesAsync();

        return Result<ICollection<ModuleAccess>>.Success(repositoryResult.Data);
    }

    public async Task<Result<ModuleAccess>> UpdateModuleAccessAsync(ModuleAccess moduleAccess)
    {
        var repositoryResult = await uow.ModuleAccesses.UpdateAsync(moduleAccess);
        
        if (!repositoryResult.IsSuccess) 
            return Result<ModuleAccess>.Failure(repositoryResult.ErrorMessage!)!; 
        
        await uow.SaveChangesAsync();

        return Result<ModuleAccess>.Success(repositoryResult.Data);
    }

    public async Task<Result> DeleteModuleAccessAsync(ModuleAccess moduleAccess)
    {
        var repositoryResult = await uow.ModuleAccesses.DeleteAsync(moduleAccess);
        
        if (!repositoryResult.IsSuccess) 
            return Result.Failure(repositoryResult.ErrorMessage!)!; 
        
        await uow.SaveChangesAsync();

        return Result.Success();
    }

    public async Task<Result<ModuleAccess?>> GetModuleAccessByIdAsync(int id)
    {
        var repositoryResult = await uow.ModuleAccesses.GetByIdAsync(id);
        return repositoryResult.IsSuccess
            ? Result<ModuleAccess?>.Success(repositoryResult.Data)
            : Result<ModuleAccess?>.Failure(repositoryResult.ErrorMessage!);
    }

    public async Task<Result<ICollection<ModuleAccess>>> GetModuleAccessesByCourseIdAsync(int courseId)
    {
        var repositoryResult = await uow.ModuleAccesses.GetAllByCourseIdAsync(courseId);
        return repositoryResult.IsSuccess
            ? Result<ICollection<ModuleAccess>>.Success(repositoryResult.Data)
            : Result<ICollection<ModuleAccess>>.Failure(repositoryResult.ErrorMessage!)!;
    }

    public async Task<Result<ICollection<ModuleAccess>>> GetModuleAccessesByModuleIdAsync(int moduleId)
    {
        var repositoryResult = await uow.ModuleAccesses.GetAllByModuleIdAsync(moduleId);
        return repositoryResult.IsSuccess
            ? Result<ICollection<ModuleAccess>>.Success(repositoryResult.Data)
            : Result<ICollection<ModuleAccess>>.Failure(repositoryResult.ErrorMessage!)!;
    }

    public async Task<Result<ICollection<ModuleAccess>>> GetModuleAccessesByUserIdAsync(int userId)
    {
        var repositoryResult = await uow.ModuleAccesses.GetAllByUserIdAsync(userId);
        return repositoryResult.IsSuccess
            ? Result<ICollection<ModuleAccess>>.Success(repositoryResult.Data)
            : Result<ICollection<ModuleAccess>>.Failure(repositoryResult.ErrorMessage!)!;
    }

    public async Task<Result<ICollection<ModuleAccess>>> GetModuleAccessesForUserByCourseIdAsync(int userId, int courseId)
    {
        var repositoryResult = await uow.ModuleAccesses.GetAllByUserIdAndCourseIdAsync(userId, courseId);
        return repositoryResult.IsSuccess
            ? Result<ICollection<ModuleAccess>>.Success(repositoryResult.Data)
            : Result<ICollection<ModuleAccess>>.Failure(repositoryResult.ErrorMessage!)!;
    }

    public async Task<Result<ICollection<ModuleAccess>>> GetAllModuleAccessesAsync()
    {
        var repositoryResult = await uow.ModuleAccesses.GetAllAsync();
        return repositoryResult.IsSuccess
            ? Result<ICollection<ModuleAccess>>.Success(repositoryResult.Data)
            : Result<ICollection<ModuleAccess>>.Failure(repositoryResult.ErrorMessage!)!;
    }
}
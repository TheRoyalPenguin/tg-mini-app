using System.Linq.Expressions;
using AutoMapper;
using Core.Interfaces.Repositories;
using Core.Models;
using Core.Utils;
using Microsoft.EntityFrameworkCore;
using Persistence.Entities;

namespace Persistence.Repositories;

public class ModuleAccessRepository(AppDbContext appDbContext, IMapper mapper) : IModuleAccessRepository
{
    public async Task<Result<ModuleAccess>> AddAsync(ModuleAccess model)
    {
        try
        {
            var newEntityEntry = await appDbContext.ModuleAccesses
                .AddAsync(mapper.Map<ModuleAccessEntity>(model));

            var newModel = mapper.Map<ModuleAccess>(newEntityEntry.Entity);
            var moduleEntity =
                await appDbContext.Modules.FirstOrDefaultAsync(m => m.Id == model.ModuleId);

            newModel.CompletedLongreadsCount = 0;
            newModel.ModuleLongreadCount = moduleEntity?.LongreadCount ?? 0;

            return Result<ModuleAccess>.Success(newModel);
        }
        catch (Exception e)
        {
            return Result<ModuleAccess>.Failure($"Failed to add access to the module: {e.Message}")!;
        }
    }

    public async Task<Result<ModuleAccess>> UpdateAsync(ModuleAccess model)
    {
        try
        {
            var moduleAccessEntity = await appDbContext.ModuleAccesses
                .FirstOrDefaultAsync(ma => ma.Id == model.Id);

            if (moduleAccessEntity == null)
                return Result<ModuleAccess>.Failure("Access entity not found")!;

            mapper.Map(model, moduleAccessEntity);

            var updatedModel = mapper.Map<ModuleAccess>(moduleAccessEntity);

            updatedModel.CompletedLongreadsCount = model.CompletedLongreadsCount;
            updatedModel.ModuleLongreadCount = model.ModuleLongreadCount;

            return Result<ModuleAccess>.Success(updatedModel);
        }
        catch (Exception e)
        {
            return Result<ModuleAccess>.Failure($"Failed to update access to the module: {e.Message}")!;
        }
    }

    public async Task<Result> DeleteAsync(ModuleAccess model)
    {
        try
        {
            var entity = await appDbContext.ModuleAccesses.FirstOrDefaultAsync(e => e.Id == model.Id);
            if (entity == null)
            {
                return Result.Failure("Module access entity not found");
            }
            
            appDbContext.ModuleAccesses.Remove(entity);
            
            return Result.Success();
        }
        catch (Exception e)
        {
            return Result.Failure($"Failed to delete access to the module: {e.Message}");
        }
    }

    public async Task<Result<ModuleAccess?>> GetByIdAsync(int id)
    {
        try
        {
            var moduleAccessEntity = await appDbContext.ModuleAccesses
                .Include(ma => ma.LongreadCompletions)
                .Include(ma => ma.Module)
                .ThenInclude(m => m.Resources)
                .AsNoTracking()
                .FirstOrDefaultAsync(ma => ma.Id == id);

            if (moduleAccessEntity == null)
                return Result<ModuleAccess?>.Success(null);

            var model = mapper.Map<ModuleAccess>(moduleAccessEntity);
            model.CompletedLongreadsCount = moduleAccessEntity.LongreadCompletions.Count;
            model.ModuleLongreadCount = moduleAccessEntity.Module.LongreadCount;

            return Result<ModuleAccess?>.Success(model);
        }
        catch (Exception e)
        {
            return Result<ModuleAccess?>.Failure($"Failed to get module access with given id: {e.Message}");
        }
    }

    public async Task<Result<ICollection<ModuleAccess>>> AddAccessesForEveryModuleForUserAsync(int userId, int courseId)
    {
        try
        {
            var modulesEntities = await appDbContext.Modules
                .Where(m => m.CourseId == courseId)
                .AsNoTracking()
                .OrderBy(m => m.Id)
                .ToListAsync();

            var moduleAccessModels = new List<ModuleAccess>();
            var moduleAccessEntities = new List<ModuleAccessEntity>();

            foreach (var module in modulesEntities)
            {
                var moduleAccessEntity = new ModuleAccessEntity
                {
                    TestTriesCount = 0,
                    IsModuleCompleted = false,
                    IsModuleAvailable = false,
                    UserId = userId,
                    ModuleId = module.Id,
                };

                var moduleAccessModel = mapper.Map<ModuleAccess>(moduleAccessEntity);
                moduleAccessModel.CompletedLongreadsCount = 0;
                moduleAccessModel.ModuleLongreadCount = module.LongreadCount;

                moduleAccessModels.Add(moduleAccessModel);
                moduleAccessEntities.Add(moduleAccessEntity);
            }

            moduleAccessEntities.First().IsModuleAvailable = true;
            moduleAccessModels.First().IsModuleAvailable = true;

            await appDbContext.ModuleAccesses.AddRangeAsync(moduleAccessEntities);

            return Result<ICollection<ModuleAccess>>.Success(moduleAccessModels);
        }
        catch (Exception e)
        {
            return Result<ICollection<ModuleAccess>>.Failure(
                $"Failed to add accesses for user {userId} in course {courseId}: {e.Message}")!;
        }
    }

    public async Task<Result<ICollection<ModuleAccess>>> GetAllAsync() 
        => await GetModuleAccessesAsync(_ => true, "Failed to get modules accesses");

    public async Task<Result<ICollection<ModuleAccess>>> GetAllByUserIdAndCourseIdAsync(int courseId, int userId)
        => await GetModuleAccessesAsync(
            ma => ma.Module.CourseId == courseId && ma.UserId == userId,
            $"Failed to get modules accesses with UserId {userId} and CourseId {courseId}");

    public async Task<Result<ICollection<ModuleAccess>>> GetAllByCourseIdAsync(int courseId)
        => await GetModuleAccessesAsync(
            ma => ma.Module.CourseId == courseId,
            $"Failed to get modules accesses with CourseId {courseId}");

    public async Task<Result<ICollection<ModuleAccess>>> GetAllByModuleIdAsync(int moduleId)
        => await GetModuleAccessesAsync(
            ma => ma.ModuleId == moduleId,
            $"Failed to get modules accesses with ModuleId {moduleId}");

    public async Task<Result<ICollection<ModuleAccess>>> GetAllByUserIdAsync(int userId)
        => await GetModuleAccessesAsync(
            ma => ma.UserId == userId,
            $"Failed to get modules accesses with UserId {userId}");

    private async Task<Result<ICollection<ModuleAccess>>> GetModuleAccessesAsync(
        Expression<Func<ModuleAccessEntity, bool>> predicate,
        string errorMessagePrefix)
    {
        try
        {
            var moduleAccesses = await RetrieveModuleAccessesAsync(predicate);
            return Result<ICollection<ModuleAccess>>.Success(moduleAccesses);
        }
        catch (Exception e)
        {
            return Result<ICollection<ModuleAccess>>.Failure($"{errorMessagePrefix}: {e.Message}")!;
        }
    }

    private async Task<ICollection<ModuleAccess>> RetrieveModuleAccessesAsync(
        Expression<Func<ModuleAccessEntity, bool>> predicate)
    {
        var entities = await appDbContext.ModuleAccesses
            .AsNoTracking()
            .Include(ma => ma.Module)
                .ThenInclude(m => m.Resources)
            .Include(ma => ma.LongreadCompletions)
            .Where(predicate)
            .ToListAsync();

        return entities.Select(entity => 
        {
            var model = mapper.Map<ModuleAccess>(entity);
            model.CompletedLongreadsCount = entity.LongreadCompletions.Count;
            model.ModuleLongreadCount = entity.Module.LongreadCount;
            return model;
        }).ToList();
    }
}
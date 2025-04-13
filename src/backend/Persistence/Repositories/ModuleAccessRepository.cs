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
            var result = await appDbContext.ModuleAccesses
                .AddAsync(mapper.Map<ModuleAccessEntity>(model));

            await appDbContext.SaveChangesAsync();

            return Result<ModuleAccess>.Success(mapper.Map<ModuleAccess>(result.Entity));
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
                return Result<ModuleAccess>.Failure("Module entity not found")!;

            mapper.Map(model, moduleAccessEntity);
            await appDbContext.SaveChangesAsync();

            var updatedModel = mapper.Map<ModuleAccess>(moduleAccessEntity);

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
            await appDbContext.ModuleAccesses
                .Where(ma => ma.Id == model.Id)
                .ExecuteDeleteAsync();

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
                .AsNoTracking()
                .FirstOrDefaultAsync(ma => ma.Id == id);

            if (moduleAccessEntity == null)
                return Result<ModuleAccess?>.Success(null);

            var model = mapper.Map<ModuleAccess>(moduleAccessEntity);

            return Result<ModuleAccess?>.Success(model);
        }
        catch (Exception e)
        {
            return Result<ModuleAccess?>.Failure($"Failed to get module access with given id: {e.Message}");
        }
    }

    public async Task<Result<ICollection<ModuleAccess>>> GetAllAsync()
    {
        try
        {
            return Result<ICollection<ModuleAccess>>.Success(
                await RetrieveModuleAccessesAsync(ma => true));
        }
        catch (Exception e)
        {
            return Result<ICollection<ModuleAccess>>.Failure($"Failed to get modules accesses: {e.Message}")!;
        }
    }

    public async Task<Result<ICollection<ModuleAccess>>> GetAllByUserIdAndCourseIdAsync(int courseId, int userId)
    {
        try
        {
            return Result<ICollection<ModuleAccess>>.Success(
                await RetrieveModuleAccessesAsync(ma => ma.Module.CourseId == courseId && ma.UserId == userId));
        }
        catch (Exception e)
        {
            return Result<ICollection<ModuleAccess>>.Failure(
                $"Failed to get modules accesses with given UserId and CourseId: {e.Message}")!;
        }
    }

    public async Task<Result<ICollection<ModuleAccess>>> GetAllByCourseIdAsync(int courseId)
    {
        try
        {
            return Result<ICollection<ModuleAccess>>.Success(
                await RetrieveModuleAccessesAsync(ma => ma.Module.CourseId == courseId));
        }
        catch (Exception e)
        {
            return Result<ICollection<ModuleAccess>>.Failure(
                $"Failed to get modules accesses with given CourseId: {e.Message}")!;
        }
    }

    public async Task<Result<ICollection<ModuleAccess>>> GetAllByModuleIdAsync(int moduleId)
    {
        try
        {
            return Result<ICollection<ModuleAccess>>.Success(
                await RetrieveModuleAccessesAsync(ma => ma.ModuleId == moduleId));
        }
        catch (Exception e)
        {
            return Result<ICollection<ModuleAccess>>.Failure(
                $"Failed to get modules accesses with given ModuleId: {e.Message}")!;
        }
    }

    public async Task<Result<ICollection<ModuleAccess>>> GetAllByUserIdAsync(int userId)
    {
        try
        {
            return Result<ICollection<ModuleAccess>>.Success(
                await RetrieveModuleAccessesAsync(ma => ma.UserId == userId));
        }
        catch (Exception e)
        {
            return Result<ICollection<ModuleAccess>>.Failure(
                $"Failed to get modules accesses with given UserId: {e.Message}")!;
        }
    }

    private async Task<ICollection<ModuleAccess>> RetrieveModuleAccessesAsync(
        Expression<Func<ModuleAccessEntity, bool>> predicate)
    {
        var query = appDbContext.ModuleAccesses
            .AsNoTracking()
            .Include(ma => ma.Module)
            .Where(predicate);

        var entities = await query.ToListAsync();
        var models = entities.Select(mapper.Map<ModuleAccess>).ToList();

        return models;
    }
}
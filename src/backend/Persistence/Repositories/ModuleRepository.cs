using AutoMapper;
using Core.Interfaces.Repositories;
using Core.Utils;
using Microsoft.EntityFrameworkCore;
using Persistence.Entities;
using Module = Core.Models.Module;

namespace Persistence.Repositories;

public class ModuleRepository(AppDbContext appDbContext, IMapper mapper) : IModuleRepository
{
    public async Task<Result<Module>> AddAsync(Module model)
    {
        try
        {
            var result = await appDbContext.Modules
                .AddAsync(mapper.Map<ModuleEntity>(model));
            
            await appDbContext.SaveChangesAsync();

            return Result<Module>.Success(mapper.Map<Module>(result.Entity));
        }
        catch (Exception e)
        {
            return Result<Module>.Failure($"Failed to add module: {e.Message}")!;
        }
    }

    public async Task<Result<Module>> UpdateAsync(Module model)
    {
        try
        {
            var moduleEntity = await appDbContext.Modules
                .FirstOrDefaultAsync(m => m.Id == model.Id);

            if (moduleEntity == null)
                return Result<Module>.Failure("Module entity not found")!;

            mapper.Map(model, moduleEntity);
            await appDbContext.SaveChangesAsync();

            var updatedModel = mapper.Map<Module>(moduleEntity);
            
            return Result<Module>.Success(updatedModel);
        }
        catch (Exception e)
        {
            return Result<Module>.Failure($"Failed to update module: {e.Message}")!;
        }
    }

    public async Task<Result> DeleteAsync(Module model)
    {
        try
        {
            await appDbContext.Modules
                .Where(m => m.Id == model.Id)
                .ExecuteDeleteAsync();
            
            return Result.Success();
        }
        catch (Exception e)
        {
            return Result.Failure($"Failed to delete module: {e.Message}");
        }
    }
    
    public async Task<Result> DeleteAsync(int id)
    {
        try
        {
            await appDbContext.Modules
                .Where(m => m.Id == id)
                .ExecuteDeleteAsync();
            
            return Result.Success();
        }
        catch (Exception e)
        {
            return Result.Failure($"Failed to delete module: {e.Message}");
        }
    }

    public async Task<Result<Module?>> GetByIdAsync(int id)
    {
        try
        {
            var moduleEntity = await appDbContext.Modules
                .AsNoTracking()
                .Include(m => m.Resources)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (moduleEntity == null)
                return Result<Module?>.Success(null);

            var model = mapper.Map<Module>(moduleEntity);

            return Result<Module?>.Success(model);
        }
        catch (Exception e)
        {
            return Result<Module?>.Failure($"Failed to get module with given id: {e.Message}");
        }
    }

    public async Task<Result<ICollection<Module>>> GetAllAsync()
    {
        try
        {
            var moduleEntities = await appDbContext.Modules
                .AsNoTracking()
                .Include(m => m.Resources)
                .ToListAsync();
            
            var models = moduleEntities
                .Select(mapper.Map<Module>)
                .ToList();

            return Result<ICollection<Module>>.Success(models);
        }
        catch (Exception e)
        {
            return Result<ICollection<Module>>.Failure($"Failed to get modules: {e.Message}")!;
        }
    }

    public async Task<Result<ICollection<Module>>> GetAllByCourseIdAsync(int courseId)
    {
        try 
        {
            var moduleEntities = await appDbContext.Modules
                .Where(m => m.CourseId == courseId)
                .AsNoTracking()
                .Include(m => m.Resources)
                .ToListAsync();

            var models = moduleEntities
                .Select(mapper.Map<Module>)
                .ToList();

            return Result<ICollection<Module>>.Success(models);
        }
        catch (Exception e)
        {
            return Result<ICollection<Module>>.Failure($"Failed to get modules with given id: {e.Message}")!;
        }
    }
    
    public async Task<Result<bool>> ExistsAsync(int moduleId)
    {
        try
        {
            bool exists = await appDbContext.Modules
                .AnyAsync(m => m.Id == moduleId);
            return Result<bool>.Success(exists);
        }
        catch (Exception ex)
        {
            return Result<bool>.Failure($"Ошибка при проверке существования модуля: {ex.Message}");
        }
    }

    public async Task<Result<bool>> ExistsForCourseAsync(int courseId, int moduleId)
    {
        try
        {
            bool exists = await appDbContext.Modules
                .AnyAsync(m => m.Id == moduleId && m.CourseId == courseId);
            return Result<bool>.Success(exists);
        }
        catch (Exception ex)
        {
            return Result<bool>.Failure($"Ошибка при проверке принадлежности модуля к курсу: {ex.Message}");
        }
    }
}
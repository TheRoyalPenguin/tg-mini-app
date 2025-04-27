using AutoMapper;
using Core.Interfaces.Repositories;
using Core.Models;
using Core.Utils;
using Microsoft.EntityFrameworkCore;
using Persistence.Entities;

namespace Persistence.Repositories;

/*public class ResourceRepository : IResourceRepository
{
    private readonly AppDbContext context;
    private readonly IMapper mapper;

    public ResourceRepository(AppDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }
    public async Task<Result<Resource>> AddAsync(Resource resource)
    {
        try
        {
            var entity = mapper.Map<ResourceEntity>(resource);
            await context.Resources.AddAsync(entity);
            await context.SaveChangesAsync();
            return Result<Resource>.Success(mapper.Map<Resource>(entity));
        }
        catch (Exception e)
        {
            return Result<Resource>.Failure($"Failed to add resource: {e.Message}");
        }
    }

    public async  Task<Result<Resource>> UpdateAsync(Resource resource)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> DeleteAsync(Resource resource)
    {
        try
        {
            var entity = mapper.Map<ResourceEntity>(resource);
            context.Resources.Remove(entity);
            await context.SaveChangesAsync();
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure($"Failed to delete resource: {ex.Message}");
        }
    }

    public async Task<Result<Resource?>> GetByIdAsync(int id)
    {
        try
        {
            var entity = await context.Resources.FindAsync(id);
            if (entity == null)
                return Result<Resource?>.Failure($"Resource with id: {id} not found");

            var resource = mapper.Map<Resource>(entity);
            return Result<Resource?>.Success(resource);
        }
        catch (Exception ex)
        {
            return Result<Resource?>.Failure($"Failed to get resource by ID: {ex.Message}");
        }
    }

    public async Task<Result<ICollection<Resource>>> GetAllAsync()
    {
        try
        {
            var entities = await context.Resources.ToListAsync();
            var resources = mapper.Map<ICollection<Resource>>(entities);
            return Result<ICollection<Resource>>.Success(resources);
        }
        catch (Exception ex)
        {
            return Result<ICollection<Resource>>.Failure($"Failed to get all resources: {ex.Message}");
        }
    }

    public async Task<Result<ICollection<Resource>>> GetAllByModuleIdAsync(int moduleId)
    {
        try
        {
            var entities = await context.Resources.Where(r => r.ModuleId == moduleId).ToListAsync();
            var resources = mapper.Map<ICollection<Resource>>(entities);
            return Result<ICollection<Resource>>.Success(resources);
        }
        catch (Exception e)
        {
            return Result<ICollection<Resource>>.Failure($"Failed to get resources: {e.Message}");
        }
    }

    public async Task<Result> DeleteAsync(int id)
    {
        try
        {
            await context.Resources
                .Where(m => m.Id == id)
                .ExecuteDeleteAsync();
            
            return Result.Success();
        }
        catch (Exception e)
        {
            return Result.Failure($"Failed to delete resource: {e.Message}");
        }
    }
}*/
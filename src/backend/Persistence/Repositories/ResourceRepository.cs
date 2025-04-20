using AutoMapper;
using Core.Interfaces.Repositories;
using Core.Models;
using Core.Utils;
using Microsoft.EntityFrameworkCore;
using Persistence.Entities;

namespace Persistence.Repositories;

public class ResourceRepository : IResourceRepository
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
        throw new NotImplementedException();
    }

    public async Task<Result<ICollection<Resource>>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<TestingQuestion>> GetAllByModuleIdAsync(int moduleId)
    {
        throw new NotImplementedException();
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
}
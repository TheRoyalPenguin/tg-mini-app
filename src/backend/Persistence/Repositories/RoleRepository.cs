using AutoMapper;
using Core.Interfaces.Repositories;
using Core.Models;
using Core.Utils;
using Microsoft.EntityFrameworkCore;
using Persistence.Entities;

namespace Persistence.Repositories;

public class RoleRepository(AppDbContext appDbContext, IMapper mapper) : IRoleRepository
{
    public async Task<Result<Role>> AddAsync(Role model)
    {
        try
        {
            var result = await appDbContext.Roles
                .AddAsync(mapper.Map<RoleEntity>(model));
            
            return Result<Role>.Success(mapper.Map<Role>(result.Entity));
        }
        catch (Exception e)
        {
            return Result<Role>.Failure($"Failed to add Role: {e.Message}")!;
        }
    }

    public async Task<Result<Role>> UpdateAsync(Role model)
    {
        try
        {
            var roleEntity = await appDbContext.Roles
                .FirstOrDefaultAsync(m => m.Id == model.Id);

            if (roleEntity == null)
                return Result<Role>.Failure("Role entity not found")!;

            mapper.Map(model, roleEntity);

            var updatedModel = mapper.Map<Role>(roleEntity);
            
            return Result<Role>.Success(updatedModel);
        }
        catch (Exception e)
        {
            return Result<Role>.Failure($"Failed to update Role: {e.Message}")!;
        }
    }

    public async Task<Result> DeleteAsync(Role model)
    {
        try
        {
            var entity = await appDbContext.Roles.FirstOrDefaultAsync(m => m.Id == model.Id);
            if (entity == null)
            {
                return Result.Failure("Role entity not found")!;
            }
            
            appDbContext.Roles
                .Remove(entity);
            
            return Result.Success();
        }
        catch (Exception e)
        {
            return Result.Failure($"Failed to delete Role: {e.Message}");
        }
    }

    public async Task<Result<Role?>> GetByIdAsync(int id)
    {
        try
        {
            var roleEntity = await appDbContext.Roles
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (roleEntity == null)
                return Result<Role?>.Success(null);

            var model = mapper.Map<Role>(roleEntity);

            return Result<Role?>.Success(model);
        }
        catch (Exception e)
        {
            return Result<Role?>.Failure($"Failed to get Role with given id: {e.Message}");
        }
    }

    public async Task<Result<ICollection<Role>>> GetAllAsync()
    {
        try
        {
            var roleEntities = await appDbContext.Roles
                .AsNoTracking()
                .ToListAsync();
            
            var models = roleEntities
                .Select(mapper.Map<Role>)
                .ToList();

            return Result<ICollection<Role>>.Success(models);
        }
        catch (Exception e)
        {
            return Result<ICollection<Role>>.Failure($"Failed to get Roles: {e.Message}")!;
        }
    }
}
using AutoMapper;
using Core.Interfaces.Repositories;
using Core.Models;
using Core.Utils;
using Microsoft.EntityFrameworkCore;
using Persistence.Entities;

namespace Persistence.Repositories;

public class UserRepository(AppDbContext appDbContext, IMapper mapper) : IUserRepository
{

    public async Task<Result> DeleteAsync(User model)
    {
        try
        {
            var entity = await appDbContext.Users
                .FirstOrDefaultAsync(e => e.Id == model.Id);
            if (entity == null)
            {
                return Result.Failure("User not found");
            }

            appDbContext.Users.Remove(entity);

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure($"Failed to delete user: {ex.Message}");
        }
    }

    public async Task<Result<User>> GetByIdAsync(int id)
    {
        try
        {
            var entity = await appDbContext.Users
                .AsNoTracking()
                .Include(u => u.Enrollments)
                .Include(u => u.ModuleAccesses)
                    .ThenInclude(ma => ma.Module)
                        .ThenInclude(m => m.Resources)
                .Include(u => u.ModuleAccesses)
                    .ThenInclude(ma => ma.LongreadCompletions)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (entity == null)
                return Result<User>.Failure("User not found")!;

            var userModel = MapUser(entity);

            return Result<User>.Success(userModel);
        }
        catch (Exception ex)
        {
            return Result<User>.Failure($"Failed to get user: {ex.Message}")!;
        }
    }

    public async Task<Result<ICollection<User>>> GetAllAsync()
    {
        try
        {
            var entities = await appDbContext.Users
                .AsNoTracking()
                .Include(u => u.Enrollments)
                .Include(u => u.ModuleAccesses)
                    .ThenInclude(ma => ma.Module)
                        .ThenInclude(m => m.Resources)
                .Include(u => u.ModuleAccesses)
                    .ThenInclude(ma => ma.LongreadCompletions)
                .ToListAsync();

            var userModels = entities.Select(MapUser).ToList();
            
            return Result<ICollection<User>>.Success(userModels);
        }
        catch (Exception ex)
        {
            return Result<ICollection<User>>.Failure($"Failed to get users: {ex.Message}")!;
        }
    }

    public async Task<Result<ICollection<User>>> GetAllByCourseIdAsync(int courseId)
    {
        try
        {
            var entities = await appDbContext.Users
                .Include(u => u.Enrollments)
                .Include(u => u.ModuleAccesses)
                    .ThenInclude(ma => ma.Module)
                        .ThenInclude(m => m.Resources)
                .Include(u => u.ModuleAccesses)
                    .ThenInclude(ma => ma.LongreadCompletions)
                .Where(u => u.Enrollments.Any(e => e.CourseId == courseId))
                .ToListAsync();
            
            foreach (var user in entities)
            {
                user.ModuleAccesses = user.ModuleAccesses
                    .Where(ma => ma.Module.CourseId == courseId)
                    .ToList();
            }

            var userModels = entities.Select(MapUser).ToList();
            
            return Result<ICollection<User>>.Success(userModels);
        }
        catch (Exception ex)
        {
            return Result<ICollection<User>>.Failure($"Failed to get users by course id: {ex.Message}")!;
        }
    }

    private User MapUser(UserEntity entity)
    {
        var userModel = mapper.Map<User>(entity);
        var moduleAccessesEntities = entity.ModuleAccesses;
        var moduleAccessesModels = moduleAccessesEntities.Select(entity =>
        {
            var model = mapper.Map<ModuleAccess>(entity);
            model.CompletedLongreadsCount = entity.LongreadCompletions.Count;
            model.CompletedLongreadsCount = entity.LongreadCompletions.Count;
            model.ModuleLongreadCount = entity.Module.LongreadCount;

            return model;
        }).ToList();

        userModel.ModuleAccesses = moduleAccessesModels;

        return userModel;
    }
}
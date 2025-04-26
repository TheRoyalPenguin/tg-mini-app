using AutoMapper;
using Core.Interfaces;
using Core.Models;
using Core.Utils;
using Microsoft.EntityFrameworkCore;
using Persistence.Entities;


namespace Persistence.Repositories;

public class EnrollmentRepository : IEnrollmentRepository
{
    private readonly AppDbContext context;
    private readonly IMapper mapper;

    public EnrollmentRepository(AppDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public async Task<Result<Enrollment>> AddAsync(Enrollment enrollment)
    {
        try
        {
            var entity = mapper.Map<EnrollmentEntity>(enrollment);
            await context.Enrollments.AddAsync(entity);

            var savedModel = mapper.Map<Enrollment>(entity);
            return Result<Enrollment>.Success(savedModel);
        }
        catch (Exception e)
        {
            return Result<Enrollment>.Failure($"Failed to add module: {e.Message}");
        }
    }

    public async Task<Result<Enrollment>> UpdateAsync(Enrollment enrollment)
    {
        try
        {
            var entity = await context.Enrollments.FindAsync(enrollment.Id);
            if (entity == null)
            {
                return Result<Enrollment>.Failure($"Enrollment with Id {enrollment.Id} not found.")!;
            }

            // Обновляем поля
            entity.IsCourseCompleted = enrollment.IsCourseCompleted;
            entity.EnrollmentDate = enrollment.EnrollmentDate;
            entity.CompletionDate = enrollment.CompletionDate.Value;
            entity.UserId = enrollment.UserId;
            entity.CourseId = enrollment.CourseId;

            var updatedModel = mapper.Map<Enrollment>(entity);
            return Result<Enrollment>.Success(updatedModel);
        }
        catch (Exception e)
        {
            return Result<Enrollment>.Failure($"Failed to update enrollment: {e.Message}")!;
        }
    }

    public async Task<Result> DeleteAsync(Enrollment entity)
    {
        try
        {
            var enrollmentEntity = await context.Enrollments.FindAsync(entity.Id);
            if (enrollmentEntity == null)
                return Result.Failure("Enrollment not found");

            context.Enrollments.Remove(enrollmentEntity);
            
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure($"Failed to delete enrollment: {ex.Message}");
        }
    }

    public async Task<Result<Enrollment?>> GetByIdAsync(int id)
    {
        try
        {
            var enrollmentEntity = await context.Enrollments.FindAsync(id);
            var entity = mapper.Map<Enrollment>(enrollmentEntity);
            return Result<Enrollment?>.Success(entity);
        }
        catch (Exception ex)
        {
            return Result<Enrollment?>.Failure($"Failed to get enrollment by id: {ex.Message}");
        }
    }

    public async Task<Result<ICollection<Enrollment>>> GetAllAsync()
    {
        try
        {
            var enrollmentsEntities = await context.Enrollments.AsNoTracking().ToListAsync();
            var enrollments = mapper.Map<ICollection<Enrollment>>(enrollmentsEntities);
            return Result<ICollection<Enrollment>>.Success(enrollments);
        }
        catch (Exception ex)
        {
            return Result<ICollection<Enrollment>>.Failure($"Failed to get all enrollments: {ex.Message}")!;
        }
    }

    public async Task<Result<ICollection<Course>>> GetCoursessByUserId(int id)
    {
        try
        {
            var courseEntities = await context.Enrollments
                .Where(e => e.UserId == id)
                .Include(e => e.Course)
                .AsNoTracking()
                .Select(e => e.Course)
                .ToListAsync();

            var courses = mapper.Map<ICollection<Course>>(courseEntities);
            return Result<ICollection<Course>>.Success(courses);
        }
        catch (Exception ex)
        {
            return Result<ICollection<Course>>.Failure($"Failed to get courses by user id: {ex.Message}")!;
        }
    }
    
    public async Task<Result<ICollection<User>>> GetUsersByCourseId(int id)
    {
        try
        {
            var userEntities = await context.Enrollments
                .Where(e => e.CourseId == id)
                .Select(e => e.User)
                .Include(u => u.ModuleAccesses.Where(ma => ma.Module.CourseId == id))
                .AsNoTracking()
                .ToListAsync();

            var users = mapper.Map<ICollection<User>>(userEntities);
            return Result<ICollection<User>>.Success(users);
        }
        catch (Exception ex)
        {
            return Result<ICollection<User>>.Failure($"Failed to get users by course id: {ex.Message}")!;
        }
    }
    
    public async Task<Result> DeleteAsync(int id)
    {
        try
        {
            var entity = await context.Enrollments.FindAsync(id);
            if (entity == null)
                return Result.Failure("Enrollment not found");

            context.Enrollments.Remove(entity);
        
            return Result.Success();
        }
        catch (Exception e)
        {
            return Result.Failure($"Failed to delete module: {e.Message}");
        }
    }
}

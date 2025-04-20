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
            await context.SaveChangesAsync();

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
                return Result<Enrollment>.Failure($"Enrollment with Id {enrollment.Id} not found.");
            }

            // Обновляем поля
            entity.IsCourseCompleted = enrollment.IsCourseCompleted;
            entity.EnrollmentDate = enrollment.EnrollmentDate;
            entity.CompletionDate = enrollment.CompletionDate.Value;
            entity.UserId = enrollment.UserId;
            entity.CourseId = enrollment.CourseId;

            await context.SaveChangesAsync();

            var updatedModel = mapper.Map<Enrollment>(entity);
            return Result<Enrollment>.Success(updatedModel);
        }
        catch (Exception e)
        {
            return Result<Enrollment>.Failure($"Failed to update enrollment: {e.Message}");
        }
    }



    public async Task<Result> DeleteAsync(Enrollment entity)
    {
        try
        {
            var enrollmentEntity = mapper.Map<EnrollmentEntity>(entity);
            context.Enrollments.Remove(enrollmentEntity);
            await context.SaveChangesAsync();
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
            return Result<ICollection<Enrollment>>.Failure($"Failed to get all enrollments: {ex.Message}");
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
            return Result<ICollection<Course>>.Failure($"Failed to get courses by user id: {ex.Message}");
        }
    }
    
    public async Task<Result> DeleteAsync(int id)
    {
        try
        {
            await context.Enrollments
                .Where(m => m.Id == id)
                .ExecuteDeleteAsync();
            
            return Result.Success();
        }
        catch (Exception e)
        {
            return Result.Failure($"Failed to delete module: {e.Message}");
        }
    }
}

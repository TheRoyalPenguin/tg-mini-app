using AutoMapper;
using Core.Interfaces.Repositories;
using Core.Models;
using Core.Utils;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class CourseRepository : ICourseRepository
{
    private readonly AppDbContext context;
    private readonly IMapper mapper;
    public CourseRepository(AppDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }
    public Task<Result<Course>> AddAsync(Course entity)
    {
        throw new NotImplementedException();
    }

    public Task<Result<Course>> UpdateAsync(Course entity)
    {
        throw new NotImplementedException();
    }

    public Task<Result> DeleteAsync(Course entity)
    {
        throw new NotImplementedException();
    }

    public Task<Result<Course?>> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<ICollection<Course>>> GetAllAsync()
    {
        try
        {
            var coursesEntities = await context.Courses.AsNoTracking().ToListAsync();
            var courses = mapper.Map<ICollection<Course>>(coursesEntities);
            return Result<ICollection<Course>>.Success(courses);
        }
        catch (Exception ex)
        {
            return Result<ICollection<Course>>.Failure($"Failed to get all courses: {ex.Message}");
        }
    }
    
    public async Task<Result<bool>> ExistsAsync(int courseId)
    {
        try
        {
            bool exists = await context.Courses
                .AnyAsync(c => c.Id == courseId);
            return Result<bool>.Success(exists);
        }
        catch (Exception ex)
        {
            return Result<bool>.Failure($"Ошибка при проверке существования курса: {ex.Message}");
        }
    }
}
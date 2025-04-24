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
}
using AutoMapper;
using Core.Interfaces;
using Core.Models;
using Microsoft.EntityFrameworkCore;

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
    public Task<Enrollment> AddAsync(Enrollment entity)
    {
        throw new NotImplementedException();
    }

    public Task<Enrollment> UpdateAsync(Enrollment entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Enrollment entity)
    {
        throw new NotImplementedException();
    }

    public Task<Enrollment?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<Enrollment>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<ICollection<Course>> GetCourseTitlesByUserId(int id)
    {
        var courseEntities = await context.Enrollments.Where(e => e.UserId == id)
            .Include(e => e.Course).AsNoTracking()
            .Select(e => e.Course).ToListAsync();
        var courses = mapper.Map<ICollection<Course>>(courseEntities);
        return courses;
    }
}
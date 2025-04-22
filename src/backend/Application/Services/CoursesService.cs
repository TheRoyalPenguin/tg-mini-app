using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Core.Utils;

namespace Application.Services;

public class CoursesService : ICourseService
{
    private readonly ICourseRepository courseRepository;

    public CoursesService(ICourseRepository courseRepository)
    {
        this.courseRepository = courseRepository;
    }

    public async Task<Result<ICollection<Course>>> GetAllAsync()
    {
        return await courseRepository.GetAllAsync();
    }
}
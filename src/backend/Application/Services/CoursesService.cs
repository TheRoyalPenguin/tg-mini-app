using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Core.Utils;

namespace Application.Services;

public class CoursesService(ICourseRepository courseRepository, IUnitOfWork uow) : ICourseService
{
    public async Task<Result<ICollection<Course>>> GetAllAsync()
    {
        return await courseRepository.GetAllAsync();
    }
}
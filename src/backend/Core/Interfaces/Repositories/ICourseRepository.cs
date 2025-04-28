using Core.Models;
using Core.Utils;

namespace Core.Interfaces.Repositories;

public interface ICourseRepository : IRepository<int, Course>
{
    Task<Result<bool>> ExistsAsync(int courseId);
}
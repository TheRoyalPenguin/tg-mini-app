using System.Collections;
using Core.Models;
using Core.Utils;

namespace Core.Interfaces.Repositories;

public interface IUserRepository
{
    Task<Result<ICollection<User>>> GetAllByCourseIdAsync(int courseId);
    Task<Result<User>> GetOneInCourseAsync(int userId, int courseId);
    Task<Result<User>> GetOneWithAllCourses(int userId);
}
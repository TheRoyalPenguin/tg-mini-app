using Core.Models;
using Core.Utils;

namespace Core.Interfaces.Services;

public interface IAdminService
{
    Task<Result<ICollection<User>>> GetUsersByCourse(int courseId);
    Task<Result> RegisterUserOnCourse(int userId, int courseId);
}
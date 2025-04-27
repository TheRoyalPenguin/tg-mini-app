using Core.Models;
using Core.Utils;

namespace Core.Interfaces.Services;

public interface IAdminService
{
    Task<Result<ICollection<User>>> GetUsersInCourse(int courseId);
    Task<Result<User>> GetConcreteUserInCourse(int userId, int courseId);
    Task<Result<User>> GetConcreteUser(int userId);
    Task<Result> RegisterUserOnCourse(int userId, int courseId);
}
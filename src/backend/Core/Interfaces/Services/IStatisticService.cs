using Core.Models;
using Core.Utils;

namespace Core.Interfaces.Services;

public interface IStatisticService
{
    Task<Result<TestResult>> AddStatisticAsync(TestResult entity);
    Task<Result<ICollection<User>>> GetUsersInCourseStatisticAsync(int courseId);
    Task<Result<User>> GetConcreteUserInCourseStatisticAsync(int userId, int courseId);
    Task<Result<User>> GetConcreteUserStatisticAsync(int userId);
}
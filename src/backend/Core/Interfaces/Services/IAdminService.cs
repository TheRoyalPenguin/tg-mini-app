using Core.Models;
using Core.Utils;

namespace Core.Interfaces.Services;

public interface IAdminService
{
    Task<Result<ICollection<User>>> GetUsersInCourse(int courseId);
    Task<Result<User>> GetConcreteUserInCourse(int userId, int courseId);
    Task<Result<User>> GetConcreteUser(int userId);
    Task<Result> RegisterUserOnCourse(int userId, int courseId);
    Task<Result<ICollection<(User user, ICollection<TestResult> testResults)>>> GetTestResultsByCourse(int courseId);
    Task<Result<(User user, ICollection<TestResult> testResults)>> GetTestResultsByUser(int userId);
    Task<Result<(User user, ICollection<TestResult> testResults)>> GetTestResultsForUserByCourse(int userId, int courseId);
}
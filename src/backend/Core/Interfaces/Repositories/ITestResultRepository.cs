using Core.Models;
using Core.Utils;

namespace Core.Interfaces.Repositories;

public interface ITestResultRepository : IRepository<int, TestResult>
{
    Task<Result<ICollection<TestResult>>> GetAllByUser(int userId);
    Task<Result<ICollection<TestResult>>> GetAllForUserByCourse(int userId, int courseId);
    Task<Result<ICollection<TestResult>>> GetAllByCourse(int courseId);
}
using Core.Models;
using Core.Utils;

namespace Core.Interfaces;

public interface IEnrollmentRepository : IRepository<int, Enrollment>
{
    Task<Result<ICollection<Course>>> GetCoursessByUserId(int id);
    Task<Result<ICollection<User>>> GetUsersByCourseId(int id);
    Task<Result> DeleteAsync(int id);
}
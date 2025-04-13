using Core.Models;

namespace Core.Interfaces;

public interface IEnrollmentRepository : IRepository<int, Enrollment>
{
    Task<ICollection<Course>> GetCourseTitlesByUserId(int id);
}
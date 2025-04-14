using Core.Utils;

namespace Core.Interfaces.Services;

public interface IEnrollmentService
{
    Task<Result<ICollection<string>>> GetCourseTitlesByUserId(int id);
}
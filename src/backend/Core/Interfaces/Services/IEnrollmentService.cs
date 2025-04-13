namespace Core.Interfaces.Services;

public interface IEnrollmentService
{
    Task<ICollection<string>> GetCourseTitlesByUserId(int id);
}
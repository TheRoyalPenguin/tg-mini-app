using Core.Models;
using Core.Utils;

namespace Core.Interfaces.Services;

public interface IEnrollmentService
{
    public interface IEnrollmentService
    {
        Task<Result<ICollection<string>>> GetCourseTitlesByUserId(int userId);
        Task<Result<Enrollment>> AddAsync(Enrollment enrollment);
        Task<Result<Enrollment>> UpdateAsync(Enrollment enrollment);
        Task<Result> DeleteAsync(Enrollment enrollment);
        Task<Result<Enrollment?>> GetByIdAsync(int id);
        Task<Result<ICollection<Enrollment>>> GetAllAsync();
    }
}
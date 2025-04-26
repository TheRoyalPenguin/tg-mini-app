using Core.Models;
using Core.Utils;

namespace Core.Interfaces.Services;

public interface IEnrollmentService
{
        Task<Result<ICollection<Course>>> GetCoursesByUserId(int id);
        Task<Result<ICollection<User>>> GetUsersByCourseId(int id);
        Task<Result<Enrollment>> AddAsync(Enrollment enrollment);
        Task<Result<Enrollment>> UpdateAsync(Enrollment enrollment);
        Task<Result> DeleteAsync(int id);
        
        Task<Result<Enrollment?>> GetByIdAsync(int id);
        Task<Result<ICollection<Enrollment>>> GetAllAsync();
}
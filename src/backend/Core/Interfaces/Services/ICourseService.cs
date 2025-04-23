using Core.Models;
using Core.Utils;

namespace Core.Interfaces.Services;

public interface ICourseService
{
    Task<Result<ICollection<Course>>> GetAllAsync();
}
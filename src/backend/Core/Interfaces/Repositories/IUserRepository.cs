using System.Collections;
using Core.Models;
using Core.Utils;

namespace Core.Interfaces.Repositories;

public interface IUserRepository
{
    Task<Result<ICollection<User>>> GetAllByCourseIdAsync(int courseId);
}
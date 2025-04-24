using Core.Models;
using Core.Utils;

namespace Core.Interfaces.Services;

public interface IAdminService
{
    public Result<ICollection<User>> GetUsersByCourse(int courseId);
}
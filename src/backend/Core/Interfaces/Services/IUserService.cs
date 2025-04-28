using Core.Models;
using Core.Utils;

namespace Core.Interfaces.Services;

public interface IUserService
{
    Task<Result> SetNotificationDaysDelay(int userId, int daysDelay);
    Task<Result<ICollection<User>>> GetUsersAsync();
}
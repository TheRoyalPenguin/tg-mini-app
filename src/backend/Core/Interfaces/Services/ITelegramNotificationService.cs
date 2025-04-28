using Core.Models;
using Core.Utils;

namespace Core.Interfaces.Services;

public interface ITelegramNotificationService
{
    Task<Result<User>> NotifyUserAboutModuleAsync(long tgId, int moduleId);
}
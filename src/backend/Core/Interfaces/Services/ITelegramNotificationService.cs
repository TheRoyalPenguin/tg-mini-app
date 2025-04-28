using Core.Models;
using Core.Utils;

namespace Core.Interfaces.Services;

public interface ITelegramNotificationService
{
    Task<Result> NotifyUserAboutModuleAsync(long tgId, int moduleId);
}

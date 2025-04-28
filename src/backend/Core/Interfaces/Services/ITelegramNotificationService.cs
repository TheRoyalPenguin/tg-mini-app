using Core.Utils;

namespace Core.Interfaces.Services;

public interface ITelegramNotificationService
{
    Task<Result> NotifyUser(long tgId, string message);
}

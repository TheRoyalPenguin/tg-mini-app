using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Utils;
using Microsoft.Extensions.Logging;
using System.Text;

namespace Application.Services;

public class TelegramNotificationService : ITelegramNotificationService
{
    private readonly IBotGateway _botGateway;
    private readonly ILogger<TelegramNotificationService> _logger;

    public TelegramNotificationService(
        IBotGateway botGateway,
        ILogger<TelegramNotificationService> logger)
    {
        _botGateway = botGateway;
        _logger = logger;
    }

    public async Task<Result> NotifyUser(long tgId, string message)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine(message);
        try
        {
            await _botGateway.SendAsync(tgId, message);
            return Result.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send notification");
            return Result.Failure("Ошибка отправки уведомления!");
        }
    }
}

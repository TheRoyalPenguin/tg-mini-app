using Bot.Handlers;
using Telegram.Bot;
using Telegram.Bot.Polling;

namespace Bot;

public class BotHostedService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ITelegramBotClient _botClient;

    public BotHostedService(ITelegramBotClient botClient, IServiceProvider serviceProvider)
    {
        _botClient = botClient;
        _serviceProvider = serviceProvider;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var scope = _serviceProvider.CreateScope();
        var updateHandler = scope.ServiceProvider.GetRequiredService<UpdateHandler>();

        _botClient.StartReceiving(
            updateHandler.HandleUpdateAsync,
            updateHandler.HandleErrorAsync,
            new ReceiverOptions(),
            cancellationToken: stoppingToken
        );
        return Task.CompletedTask;
    }
}

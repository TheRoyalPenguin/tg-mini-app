using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace LevelUpTgBot.Handlers;

public class UpdateHandler
{
    private readonly MessageHandler _messageHandler;

    public UpdateHandler(MessageHandler messageHandler)
    {
        _messageHandler = messageHandler;
    }

    public async Task HandleUpdateAsync(ITelegramBotClient client, Update update, CancellationToken token)
    {
        if (update?.Message != null)
        {
            await _messageHandler.HandleMessageAsync(update.Message, token);
        }
    }

    public Task HandleErrorAsync(ITelegramBotClient client, Exception exception, HandleErrorSource source, CancellationToken token)
    {
        Console.WriteLine($"Ошибка: {exception.Message}");
        return Task.CompletedTask;
    }
}

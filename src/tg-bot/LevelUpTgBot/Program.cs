using LevelUpTgBot.Handlers;
using LevelUpTgBot.Services;
using Telegram.Bot;
using Telegram.Bot.Polling;

namespace LevelUpTgBot;

public class Program
{
    private const string Token = "";
    private const string WebAppUrl = "https://levelupapp.hopto.org/";

    public static async Task Main(string[] args)
    {
        var botClient = new TelegramBotClient(Token);
        var backendService = new BackendService(new HttpClient(), "http://backend:5000/");
        var telegramService = new TelegramService(botClient);
        var commandHandler = new CommandHandler(telegramService, WebAppUrl);
        var messageHandler = new MessageHandler(commandHandler, telegramService, backendService);
        var updateHandler = new UpdateHandler(messageHandler);

        var me = await botClient.GetMe();
        Console.WriteLine($"Бот запущен: {me.FirstName}");

        using var cts = new CancellationTokenSource();

        botClient.StartReceiving(
            updateHandler.HandleUpdateAsync,
            updateHandler.HandleErrorAsync,
            new ReceiverOptions(),
            cancellationToken: cts.Token
        );

        await Task.Delay(-1, cts.Token);
    }
}

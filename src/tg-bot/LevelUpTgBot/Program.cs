using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace LevelUpTgBot;

public class Program
{
    static readonly string token = "";
    static async Task Main(string[] args)
    {
        var botCl = new TelegramBotClient(token);
        var me = await botCl.GetMe();
        Console.WriteLine("Бот успешно запущен: " + me.FirstName);

        using var cts = new CancellationTokenSource();

        botCl.StartReceiving(
            HandleUpdateAsync,
            HandleErrorAsync,
            new ReceiverOptions(),
            cancellationToken: cts.Token
            );

        Console.WriteLine("Нажмите клавишу enter, чтобы остановить бота.");
        Console.ReadLine();
        cts.Cancel();
    }

    private static Task HandleErrorAsync(ITelegramBotClient client, Exception exception, HandleErrorSource source, CancellationToken token)
    {
        Console.WriteLine("Произошла ошибка: " + exception.Message);
        return Task.CompletedTask;
    }

    private static async Task HandleUpdateAsync(ITelegramBotClient client, Update update, CancellationToken token)
    {
        if (update?.Message == null)
            return;

        if (update.Type == UpdateType.Message)
        {
            switch(update.Message.Type)
            {
                case MessageType.Text:
                    await HandleTextMessage(client, update.Message, token);
                    break;
                case MessageType.Contact:
                    await HandleContactMessage(client, update.Message, token);
                    break;
                default:
                    break;
            }
        }
    }

    private static async Task HandleTextMessage(ITelegramBotClient client, Message message, CancellationToken token)
    {
        var chatId = message.Chat?.Id;
        var userId = message.From?.Id;

        if (chatId == null || userId == null)
            return;

        var messageText = message.Text ?? string.Empty;
        Console.WriteLine(userId);

        if (messageText.StartsWith("/start"))
        {
            await HandleStartCommand(client, chatId, token);
            return;
        }
        await SafeSendMessageAsync(client, chatId!, "Привет, вот список команд: /start", token);
    }

    private static async Task HandleStartCommand(ITelegramBotClient client, long? chatId, CancellationToken token)
    {
        var keyboard = new ReplyKeyboardMarkup(new[]
        {
            new KeyboardButton[]
            {
                new KeyboardButton("Отправить номер телефона")
                {
                    RequestContact = true
                }
            }
        })
        {
            ResizeKeyboard = true,
            OneTimeKeyboard = true
        };

        await SafeSendMessageAsync(client, chatId!, "Добро пожаловать! Пожалуйста, отправьте ваш номер телефона, нажав кнопку ниже.", token, keyboard);
    }

    private static async Task HandleContactMessage(ITelegramBotClient client, Message message, CancellationToken token)
    {
        var chatId = message.Chat?.Id;
        var contact = message.Contact;

        if (chatId == null)
            return;

        if (contact != null)
        {
            Console.WriteLine($"Получен номер телефона от пользователя {contact.FirstName}: {contact.PhoneNumber}");
            await SafeSendMessageAsync(client, chatId, "Спасибо, номер телефона получен!", token);
        }
        else
        {
            await SafeSendMessageAsync(client, chatId, "Контакт не получен. Попробуйте снова.", token);
        }
    }

    private static async Task<bool> SafeSendMessageAsync(ITelegramBotClient client, ChatId chatId, string text, CancellationToken token, ReplyMarkup? replyMarkup = null)
    {
        try
        {
            if (replyMarkup == null)
            {
                await client.SendMessage(
                    chatId: chatId,
                    text: text,
                    cancellationToken: token
                );
            }
            else
            {
                await client.SendMessage(
                    chatId: chatId,
                    text: text,
                    replyMarkup: replyMarkup,
                    cancellationToken: token
                );
            }
            return true;
        }
        catch (ApiRequestException ex) when (ex.Message.Contains("bot was blocked by the user"))
        {
            Console.WriteLine($"Бот заблокирован пользователем. ChatId: {chatId}");
            return false;
        }
        catch (ApiRequestException ex)
        {
            Console.WriteLine($"Ошибка API: {ex.Message}");
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Непредвиденная ошибка: {ex.Message}");
            return false;
        }
    }
}

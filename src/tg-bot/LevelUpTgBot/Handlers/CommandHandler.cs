using LevelUpTgBot.Interfaces;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;

namespace LevelUpTgBot.Handlers;

public class CommandHandler
{
    private readonly ITelegramService _telegramService;
    public string WebAppUrl { get; }

    public CommandHandler(ITelegramService telegramService, string webAppUrl)
    {
        _telegramService = telegramService;
        WebAppUrl = webAppUrl;
    }

    public async Task HandleTextCommand(Message message, CancellationToken token)
    {
        var chatId = message.Chat.Id;
        var text = message.Text?.Trim() ?? "";

        if (text.StartsWith("/start"))
        {
            var param = text.Length > 6 ? text.Substring(7) : null;

            if (param == null)
            {
                await _telegramService.SafeSendMessageAsync(chatId, "Добро пожаловать!", token);
            }

            await RequestPhone(chatId, token);
        }
        else
        {
            await _telegramService.SafeSendMessageAsync(chatId, "Привет, вот список команд: /start", token);
        }
    }

    public async Task ResendPhoneRequest(ChatId chatId, CancellationToken token)
    {
        await _telegramService.SafeSendMessageAsync(chatId, "Контакт не получен. Попробуйте снова.", token);
        await RequestPhone(chatId, token);
    }

    private async Task RequestPhone(ChatId chatId, CancellationToken token)
    {
        var keyboard = new ReplyKeyboardMarkup(new[]
        {
            new KeyboardButton[]
            {
                new KeyboardButton("Отправить номер телефона") { RequestContact = true }
            }
        })
        {
            ResizeKeyboard = true,
            OneTimeKeyboard = true
        };

        await _telegramService.SafeSendMessageAsync(chatId, "Пожалуйста, отправьте ваш номер телефона для авторизации.", token, keyboard);
    }
}

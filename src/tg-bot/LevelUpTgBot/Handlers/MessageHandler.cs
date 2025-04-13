using LevelUpTgBot.Interfaces;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;

namespace LevelUpTgBot.Handlers;

public class MessageHandler
{
    private readonly CommandHandler _commandHandler;
    private readonly ITelegramService _telegramService;
    private readonly IBackendService _backendService;

    public MessageHandler(CommandHandler commandHandler, ITelegramService telegramService, IBackendService backendService)
    {
        _commandHandler = commandHandler;
        _telegramService = telegramService;
        _backendService = backendService;
    }

    public async Task HandleMessageAsync(Message message, CancellationToken token)
    {
        switch (message.Type)
        {
            case MessageType.Text:
                await _commandHandler.HandleTextCommand(message, token);
                break;
            case MessageType.Contact:
                await HandleContactMessage(message, token);
                break;
        }
    }

    private async Task HandleContactMessage(Message message, CancellationToken token)
    {
        var contact = message.Contact;
        var chatId = message.Chat.Id;

        if (contact == null)
        {
            await _commandHandler.ResendPhoneRequest(chatId, token);
            return;
        }

        var userInfo = new
        {
            telegramId = contact.UserId,
            phone = contact.PhoneNumber,
            firstName = contact.FirstName,
            lastName = contact.LastName
        };

        //bool isSuccess = await _backendService.SendDataAsync(userInfo, "api/auth/telegram-bot");
        bool isSuccess = true;
        if (!isSuccess)
        {
            await _telegramService.SafeSendMessageAsync(chatId, "Контакт не получен. Попробуйте снова.", token);
            await _commandHandler.ResendPhoneRequest(chatId, token);
            return;
        }

        await _telegramService.SafeSendMessageAsync(chatId, "Спасибо, номер телефона получен!", token);

        var button = InlineKeyboardButton.WithWebApp("Открыть мини-приложение", _commandHandler.WebAppUrl);
        var markup = new InlineKeyboardMarkup(button);

        await _telegramService.SafeSendMessageAsync(chatId, "Нажмите кнопку ниже, чтобы открыть мини-приложение:", token, markup);
    }
}


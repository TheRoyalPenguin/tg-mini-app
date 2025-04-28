using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;

namespace Bot.Interfaces;

public interface ITelegramService
{
    Task<bool> SafeSendMessageAsync(ChatId chatId, string text, CancellationToken token, ReplyMarkup? markup = null);
}

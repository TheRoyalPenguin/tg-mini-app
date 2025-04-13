using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;

namespace LevelUpTgBot.Interfaces;

public interface ITelegramService
{
    Task<bool> SafeSendMessageAsync(ChatId chatId, string text, CancellationToken token, ReplyMarkup? markup = null);
}

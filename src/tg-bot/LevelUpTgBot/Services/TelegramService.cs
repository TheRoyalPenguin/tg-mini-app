using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using LevelUpTgBot.Interfaces;

namespace LevelUpTgBot.Services;

public class TelegramService : ITelegramService
{
    private readonly ITelegramBotClient _client;

    public TelegramService(ITelegramBotClient client)
    {
        _client = client;
    }

    public async Task<bool> SafeSendMessageAsync(ChatId chatId, string text, CancellationToken token, ReplyMarkup? replyMarkup = null)
    {
        try
        {
            if (replyMarkup == null)
            {
                await _client.SendMessage(
                    chatId: chatId,
                    text: text,
                    cancellationToken: token
                );
            }
            else
            {
                await _client.SendMessage(
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

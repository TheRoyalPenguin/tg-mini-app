using Core.Models;
using Core.Utils;

namespace Core.Interfaces.Services;

public interface ITelegramAuthService
{
    Task<Result<User>> AuthenticateViaBotAsync(long tgId, string name, string surname, string phoneNumber);
    Task<Result<User>> AuthenticateViaMiniAppAsync(long tgId, string name, string surname, string patronymic);
}

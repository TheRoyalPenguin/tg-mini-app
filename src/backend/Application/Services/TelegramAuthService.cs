using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Core.Utils;

namespace Application.Services;

public class TelegramAuthService : ITelegramAuthService
{
    private readonly ITelegramUserRepository _telegramUserRepository;
    public TelegramAuthService(ITelegramUserRepository telegramUserRepository)
    {
        _telegramUserRepository = telegramUserRepository;
    }

    public async Task<Result<User>> AuthenticateViaBotAsync(long tgId, string name, string surname, string phoneNumber)
    {
        var user = await _telegramUserRepository.GetByTelegramIdAsync(tgId);

        if (user == null)
        {
            var newUser = new User
            {
                TgId = tgId,
                PhoneNumber = phoneNumber,
                Name = name,
                Surname = surname,
                Patronymic = "",
                RoleId = 0,
                RegisteredAt = DateTime.UtcNow
            };

            await _telegramUserRepository.AddAsync(newUser);
        }
        else
        {
            user.PhoneNumber = phoneNumber;
            user.Name = string.IsNullOrEmpty(user.Name) ? name : user.Name;
            user.Surname = string.IsNullOrEmpty(user.Surname) ? surname : user.Surname;
        }

        await _telegramUserRepository.SaveChangesAsync();
        return Result<User>.Success(user);
    }

    public async Task<Result<User>> AuthenticateViaMiniAppAsync(long tgId, string name, string surname, string patronymic, string phoneNumber)
    {
        var user = await _telegramUserRepository.GetByTelegramIdAsync(tgId);

        if (user == null)
        {

            var newUser = new User
            {
                TgId = tgId,
                PhoneNumber = phoneNumber,
                Name = name,
                Surname = surname,
                Patronymic = patronymic,
                RoleId = 0,
                RegisteredAt = DateTime.UtcNow
            };

            await _telegramUserRepository.AddAsync(user);
        }
        else
        {
            user.Name = string.IsNullOrEmpty(user.Name) ? name : user.Name;
            user.Surname = string.IsNullOrEmpty(user.Surname) ? surname : user.Surname;
            user.Patronymic = string.IsNullOrEmpty(user.Patronymic) ? patronymic : user.Patronymic;
            user.PhoneNumber = string.IsNullOrEmpty(user.PhoneNumber) ? phoneNumber : user.PhoneNumber;
        }

        await _telegramUserRepository.SaveChangesAsync();

        return Result<User>.Success(user);
    }
}

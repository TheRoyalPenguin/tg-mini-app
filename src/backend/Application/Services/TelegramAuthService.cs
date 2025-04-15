using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;

namespace Application.Services;

public class TelegramAuthService : ITelegramAuthService
{
    private readonly ITelegramUserRepository _telegramUserRepository;
    public TelegramAuthService(ITelegramUserRepository telegramUserRepository)
    {
        _telegramUserRepository = telegramUserRepository;
    }

    public async Task AuthenticateViaBotAsync(long TgId, string Name, string Surname, string PhoneNumber)
    {
        var user = await _telegramUserRepository.GetByTelegramIdAsync(TgId);

        if (user == null)
        {
            var newUser = new User
            {
                TgId = TgId,
                PhoneNumber = PhoneNumber,
                Name = Name,
                Surname = Surname,
                Patronymic = "",
                RoleId = 0,
                RegisteredAt = DateTime.UtcNow
            };

            await _telegramUserRepository.AddAsync(newUser);
        }
        else
        {
            user.PhoneNumber = PhoneNumber;
            user.Name = string.IsNullOrEmpty(user.Name) ? Name : user.Name;
            user.Surname = string.IsNullOrEmpty(user.Surname) ? Surname : user.Surname;
        }

        await _telegramUserRepository.SaveChangesAsync();
    }
}

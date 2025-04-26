using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Core.Utils;

namespace Application.Services;

public class TelegramAuthService(IUnitOfWork uow) : ITelegramAuthService
{
    public async Task<Result<User>> AuthenticateViaBotAsync(long tgId, string name, string surname, string phoneNumber)
    {
        var user = await uow.TelegramUsers.GetByTelegramIdAsync(tgId);

        if (user == null)
        {
            user = new User
            {
                TgId = tgId,
                PhoneNumber = phoneNumber,
                Name = name,
                Surname = surname,
                Patronymic = "",
                RoleId = 1,
                RegisteredAt = DateTime.UtcNow
            };

            await uow.TelegramUsers.AddAsync(user);
        }
        else
        {
            user.PhoneNumber = phoneNumber;
            user.Name = string.IsNullOrEmpty(user.Name) ? name : user.Name;
            user.Surname = string.IsNullOrEmpty(user.Surname) ? surname : user.Surname;
            user.RegisteredAt = DateTime.UtcNow;
        }

        await uow.SaveChangesAsync();
        return Result<User>.Success(user);
    }

    public async Task<Result<User>> AuthenticateViaMiniAppAsync(long tgId, string name, string surname, string patronymic)
    {
        var user = await uow.TelegramUsers.GetByTelegramIdAsync(tgId);

        if (user == null)
        {

            user = new User
            {
                TgId = tgId,
                PhoneNumber = "",
                Name = name,
                Surname = surname,
                Patronymic = patronymic,
                RoleId = 1,
                RegisteredAt = DateTime.UtcNow
            };

            await uow.TelegramUsers.AddAsync(user);
        }
        else
        {
            user.Name = string.IsNullOrEmpty(user.Name) ? name : user.Name;
            user.Surname = string.IsNullOrEmpty(user.Surname) ? surname : user.Surname;
            user.Patronymic = string.IsNullOrEmpty(user.Patronymic) ? patronymic : user.Patronymic;
            user.RegisteredAt = DateTime.UtcNow;
        }

        await uow.SaveChangesAsync();

        return Result<User>.Success(user);
    }
}

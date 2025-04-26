using Core.Models;

namespace Core.Interfaces.Repositories;

public interface ITelegramUserRepository
{
    Task<User?> GetByTelegramIdAsync(long telegramId);
    Task AddAsync(User user);
    Task UpdateAsync(User user);
}

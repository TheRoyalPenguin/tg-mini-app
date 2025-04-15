namespace Core.Interfaces.Services;

public interface ITelegramAuthService
{
    Task AuthenticateViaBotAsync(long TgId, string Name, string Surname, string PhoneNumber);
}

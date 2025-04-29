namespace Core.Interfaces.Services;

public interface IBotGateway
{
    Task SendAsync(long chatId, string message);
}

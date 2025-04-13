namespace LevelUpTgBot.Interfaces;

public interface IBackendService
{
    Task<bool> SendDataAsync(object data, string endpoint);
}

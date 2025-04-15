using System.Text.Json;
using System.Text;
using LevelUpTgBot.Interfaces;

namespace LevelUpTgBot.Services;

public class BackendService :IBackendService
{
    private readonly HttpClient _httpClient;

    public BackendService(HttpClient httpClient, string baseAdress)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(baseAdress);
    }

    public async Task<bool> SendDataAsync(object data, string endpoint)
    {
        if (string.IsNullOrWhiteSpace(endpoint))
        {
            Console.WriteLine("Endpoint не может быть пустым", nameof(endpoint));
            return false;
        }

        try
        {
            var jsonData = JsonSerializer.Serialize(data);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(endpoint, content);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }
}

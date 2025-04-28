using Core.Interfaces.Services;
using System.Net.Http.Json;

namespace Application.Services;

public class BotGateway : IBotGateway
{
    private readonly HttpClient _http;
    public BotGateway(HttpClient http)
    {
        _http = http;
    }

    public async Task SendAsync(long chatId, string message)
    {
        var payload = new SendRequest
        {
            ChatId = chatId,
            Message = message
        };
        var response = await _http.PostAsJsonAsync("api/notifications/send", payload);
        response.EnsureSuccessStatusCode();
    }

    private class SendRequest
    {
        public long ChatId { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}

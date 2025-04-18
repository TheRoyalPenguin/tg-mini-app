using API.DTO;
using Core.Interfaces.Services;
using Core.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;

namespace API.Controllers;

[Route("api/auth")]
[ApiController]
public class TelegramAuthController : ControllerBase
{
    private readonly ITelegramAuthService _telegramAuthService;
    private readonly IJwtService _jwtService;
    private readonly IConfiguration _configuration;

    public TelegramAuthController(ITelegramAuthService telegramAuthService, IJwtService jwtService, IConfiguration configuration)
    {
        _telegramAuthService = telegramAuthService;
        _jwtService = jwtService;
        _configuration = configuration;
    }

    [HttpPost("telegram-bot")]
    public async Task<Result> AuthFromBot([FromBody] TelegramBotAuthRequest request)
    {
        await _telegramAuthService.AuthenticateViaBotAsync(request.TgId, request.Name, request.Surname, request.PhoneNumber);
        return Result.Success();
    }

    [HttpPost("telegram-mini-app")]
    public async Task<Result<string>> AuthFromMiniApp([FromBody] InitDataRequest request)
    {
        var botToken = _configuration["Bot:Token"];

        if (string.IsNullOrEmpty(botToken))
        {
            Console.WriteLine("Токен пустой или null!");
        }

        bool isValid = TelegramAuthHelper.ValidateTelegramData(request.InitData, botToken!);

        if (!isValid)
        {
            return Result<string>.Failure("Ошибка авторизации!")!;
        }

        var query = QueryHelpers.ParseQuery(request.InitData);
        long tgId = long.Parse(GetRequired(query, "id"));
        string firstName = GetRequired(query, "first_name");
        string lastName = query.TryGetValue("last_name", out StringValues lv) ? lv.ToString() : null;
        string username = query.TryGetValue("username", out StringValues uv) ? uv.ToString() : null;

        var result = await _telegramAuthService.AuthenticateViaMiniAppAsync(
            tgId,
            request.Name,
            request.Surname,
            request.Patronymic);

        var user = result.Data;
        var token = _jwtService.GenerateJwtToken(user).Data;

        return Result<string>.Success(token);
    }

    private static string GetRequired(Dictionary<string, StringValues> query, string key)
    {
        if (!query.TryGetValue(key, out var val) || StringValues.IsNullOrEmpty(val))
            throw new ArgumentException($"Отсутствует обязательный параметр initData: {key}");
        return val.ToString();
    }
}

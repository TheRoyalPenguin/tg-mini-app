using API.DTO;
using Core.Interfaces.Services;
using Core.Utils;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<Result<string>> AuthFromMiniApp([FromBody] TelegramMiniAppAuthRequest request)
    {
        var botToken = _configuration["Bot:Token"];

        if (string.IsNullOrEmpty(botToken))
        {
            Console.WriteLine("Токен пустой или null!");
        }

        bool isValid = TelegramAuthHelper.ValidateTelegramData(request.TgId, request.Name,
            request.Surname, request.Patronymic, request.PhoneNumber, request.Hash, botToken!);

        if (!isValid)
        {
            return Result<string>.Failure("Ошибка авторизации!")!;
        }

        var result = await _telegramAuthService.AuthenticateViaMiniAppAsync(
            request.TgId,
            request.Name,
            request.Surname,
            request.Patronymic,
            request.PhoneNumber);

        var user = result.Data;
        var token = _jwtService.GenerateJwtToken(user).Data;

        return Result<string>.Success(token);
    }
}

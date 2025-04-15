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
    
    public TelegramAuthController(ITelegramAuthService telegramAuthService)
    {
        _telegramAuthService = telegramAuthService;
    }

    [HttpPost("telegram-bot")]
    public async Task<Result> AuthFromBot([FromBody] TelegramBotAuthRequest request)
    {
        await _telegramAuthService.AuthenticateViaBotAsync(request.TgId, request.Name, request.Surname, request.PhoneNumber);
        return Result.Success();
    }
}

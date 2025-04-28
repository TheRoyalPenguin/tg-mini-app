using Bot.Interfaces;
using Bot.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bot.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationsController : ControllerBase
{
    private readonly ITelegramService _telegramService;

    public NotificationsController(ITelegramService telegramService)
    {
        _telegramService = telegramService;
    }

    public class SendRequest
    {
        public long ChatId { get; set; }
        public string Message { get; set; } = string.Empty;
    }

    [HttpPost("send")]
    public async Task<IActionResult> Send([FromBody] SendRequest request)
    {
        if (request.ChatId == 0 || string.IsNullOrWhiteSpace(request.Message))
            return BadRequest("Invalid request");

        var text = request.Message;

        await _telegramService.SafeSendMessageAsync(request.ChatId, text, new CancellationToken());

        return Ok();
    }
}

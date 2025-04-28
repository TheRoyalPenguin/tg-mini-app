using System.Net;
using API.DTO.User;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class UserController(IUserService userService) : ControllerBase
{
    [HttpPatch]
    [Route("[controller]/notification-delay/{daysDelay:int}")]
    public async Task<IActionResult> SetNotificationDaysDelay([FromBody] UserSetNotificationDaysDelayRequest request)
    {
        var serviceResult = await userService.SetNotificationDaysDelay(request.UserId, request.NotificationDaysDelay);
        if (!serviceResult.IsSuccess)
            return Problem(serviceResult.ErrorMessage);

        return Ok();
    }
}
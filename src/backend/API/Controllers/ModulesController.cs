using AutoMapper;
using Core.Interfaces.Services;
using Core.Utils;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/courses/{courseId}/[controller]")]
public class ModulesController(
    IModuleService moduleService,
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetModulesByCourseIdAsync(int courseId)
    {
        if (!User.TryGetUserId(out var userId))
            return Unauthorized();

        var serviceResult = await moduleService.GetModulesByCourseIdWithAccessAsync(courseId, userId);
        
        return serviceResult.IsSuccess 
            ? Ok(serviceResult.Data) 
            : Problem(serviceResult.ErrorMessage);
    }
}
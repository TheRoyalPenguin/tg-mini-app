using AutoMapper;
using Core.Interfaces.Services;
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
        var serviceResult = await moduleService.GetModulesByCourseIdAsync(courseId);
        
        return serviceResult.IsSuccess 
            ? Ok(serviceResult.Data) 
            : Problem(serviceResult.ErrorMessage);
    }
}
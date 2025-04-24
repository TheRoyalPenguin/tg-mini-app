using AutoMapper;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ModuleController(
    IModuleService moduleService,
    IMapper mapper) : ControllerBase
{
    
    [HttpGet("{moduleId:int}")]
    public async Task<IActionResult> GetModuleByIdAsync(int moduleId)
    {
        var serviceResult = await moduleService.GetModuleByIdAsync(moduleId);
        
        return serviceResult.IsSuccess 
            ? Ok(serviceResult.Data) 
            : Problem(serviceResult.ErrorMessage);
    }
    
    [HttpGet("courses/{courseId:int}")]
    public async Task<IActionResult> GetModulesByCourseIdAsync(int courseId)
    {
        var serviceResult = await moduleService.GetModulesByCourseIdAsync(courseId);
        
        return serviceResult.IsSuccess 
            ? Ok(serviceResult.Data) 
            : Problem(serviceResult.ErrorMessage);
    }
}
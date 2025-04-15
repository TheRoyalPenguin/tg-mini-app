using API.DTO;
using API.DTO.ModuleRequests;
using AutoMapper;
using Core.Interfaces.Services;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class ModuleController(
    IModuleService moduleService,
    IMapper mapper) : ControllerBase
{
    [HttpGet("All")]
    public async Task<IActionResult> GetAll()
    {
        var serviceResult = await moduleService.GetAllModulesAsync();

        return serviceResult.IsSuccess 
            ? Ok(serviceResult.Data)
            : Problem(serviceResult.ErrorMessage);
    }
    
    [HttpPost("New")]
    public async Task<IActionResult> Create([FromBody] NewModuleRequest request)
    {
        var model = mapper.Map<Module>(request);

        var serviceResult = await moduleService.AddModuleAsync(model);
        return serviceResult.IsSuccess 
            ? Ok(serviceResult.Data) 
            : Problem(serviceResult.ErrorMessage);
    }
    
    [HttpPut("Update")]
    public async Task<IActionResult> Update([FromBody] UpdateModuleRequest request)
    {
        var model = mapper.Map<Module>(request);

        var serviceResult = await moduleService.UpdateModuleAsync(model);
        return serviceResult.IsSuccess 
            ? Ok(serviceResult.Data) 
            : Problem(serviceResult.ErrorMessage);
    }
    
    [HttpDelete("Delete/{moduleId:int}")]
    public async Task<IActionResult> Delete([FromRoute] int moduleId)
    {
        var serviceResult = await moduleService.DeleteModuleAsync(moduleId);
        
        return serviceResult.IsSuccess 
            ? Ok() 
            : Problem(serviceResult.ErrorMessage);
    }
    
    [HttpGet("GetById/{moduleId:int}")]
    public async Task<IActionResult> GetModuleById([FromRoute] int moduleId)
    {
        var serviceResult = await moduleService.GetModuleByIdAsync(moduleId);
        
        return serviceResult.IsSuccess 
            ? Ok(serviceResult.Data) 
            : Problem(serviceResult.ErrorMessage);
    }
    
    [HttpGet("GetByCourseId/{courseId:int}")]
    public async Task<IActionResult> GetModulesByCourseId([FromRoute] int courseId)
    {
        var serviceResult = await moduleService.GetModulesByCourseIdAsync(courseId);
        
        return serviceResult.IsSuccess 
            ? Ok(serviceResult.Data) 
            : Problem(serviceResult.ErrorMessage);
    }
}
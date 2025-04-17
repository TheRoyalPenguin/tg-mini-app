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
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var serviceResult = await moduleService.GetAllModulesAsync();

        return serviceResult.IsSuccess 
            ? Ok(serviceResult.Data)
            : Problem(serviceResult.ErrorMessage);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] NewModuleRequest request)
    {
        var model = mapper.Map<Module>(request);

        var serviceResult = await moduleService.AddModuleAsync(model);
        return serviceResult.IsSuccess 
            ? Ok(serviceResult.Data) 
            : Problem(serviceResult.ErrorMessage);
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateModuleRequest request)
    {
        var model = mapper.Map<Module>(request);

        var serviceResult = await moduleService.UpdateModuleAsync(model);
        return serviceResult.IsSuccess 
            ? Ok(serviceResult.Data) 
            : Problem(serviceResult.ErrorMessage);
    }
    
    [HttpDelete("{moduleId:int}")]
    public async Task<IActionResult> DeleteAsync(int moduleId)
    {
        var serviceResult = await moduleService.DeleteModuleAsync(moduleId);
        
        return serviceResult.IsSuccess 
            ? Ok() 
            : Problem(serviceResult.ErrorMessage);
    }
    
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
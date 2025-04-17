using API.DTO.ModuleAccessRequests;
using AutoMapper;
using Core.Interfaces.Services;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class ModuleAccessController(
    IModuleAccessService moduleAccessService,
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var serviceResult = await moduleAccessService.GetAllModuleAccessesAsync();

        return serviceResult.IsSuccess 
            ? Ok(serviceResult.Data)
            : Problem(serviceResult.ErrorMessage);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] NewModuleAccessRequest request)
    {
        var model = mapper.Map<ModuleAccess>(request);

        var serviceResult = await moduleAccessService.AddModuleAccessAsync(model);
        return serviceResult.IsSuccess 
            ? Ok(serviceResult.Data) 
            : Problem(serviceResult.ErrorMessage);
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateModuleAccessRequest request)
    {
        var model = mapper.Map<ModuleAccess>(request);

        var serviceResult = await moduleAccessService.UpdateModuleAccessAsync(model);
        return serviceResult.IsSuccess 
            ? Ok(serviceResult.Data) 
            : Problem(serviceResult.ErrorMessage);
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteAsync([FromBody] DeleteModuleAccessRequest request)
    {
        var model = mapper.Map<ModuleAccess>(request);
        
        var serviceResult = await moduleAccessService.DeleteModuleAccessAsync(model);
        return serviceResult.IsSuccess 
            ? Ok() 
            : Problem(serviceResult.ErrorMessage);
    }
    
    [HttpGet("{moduleAccessId:int}")]
    public async Task<IActionResult> GetModuleAccessByIdAsync([FromRoute] int moduleAccessId)
    {
        var serviceResult = await moduleAccessService.GetModuleAccessByIdAsync(moduleAccessId);
        
        return serviceResult.IsSuccess 
            ? Ok(serviceResult.Data) 
            : Problem(serviceResult.ErrorMessage);
    }
    
    [HttpGet("courses/{courseId:int}")]
    public async Task<IActionResult> GetModuleAccessesByCourseIdAsync([FromRoute] int courseId)
    {
        var serviceResult = await moduleAccessService.GetModuleAccessesByCourseIdAsync(courseId);
        
        return serviceResult.IsSuccess 
            ? Ok(serviceResult.Data) 
            : Problem(serviceResult.ErrorMessage);
    }
    
    [HttpGet("modules/{moduleId:int}")]
    public async Task<IActionResult> GetModuleAccessesByModuleIdAsync([FromRoute] int moduleId)
    {
        var serviceResult = await moduleAccessService.GetModuleAccessesByModuleIdAsync(moduleId);
        
        return serviceResult.IsSuccess 
            ? Ok(serviceResult.Data) 
            : Problem(serviceResult.ErrorMessage);
    }
    
    [HttpGet("users/{userId:int}")]
    public async Task<IActionResult> GetModuleAccessesByUserIdAsync([FromRoute] int userId)
    {
        var serviceResult = await moduleAccessService.GetModuleAccessesByUserIdAsync(userId);
        
        return serviceResult.IsSuccess 
            ? Ok(serviceResult.Data) 
            : Problem(serviceResult.ErrorMessage);
    }
    
    [HttpGet("users/{userId:int}/courses/{courseId:int}")]
    public async Task<IActionResult> GetModuleAccessesForUserByCourseIdAsync([FromRoute] int userId, [FromRoute] int courseId)
    {
        var serviceResult = await moduleAccessService.GetModuleAccessesForUserByCourseIdAsync(userId, courseId);
        
        return serviceResult.IsSuccess 
            ? Ok(serviceResult.Data) 
            : Problem(serviceResult.ErrorMessage);
    }
}
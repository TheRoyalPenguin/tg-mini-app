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
    [HttpGet("All")]
    public async Task<IActionResult> GetAll()
    {
        var serviceResult = await moduleAccessService.GetAllModuleAccessesAsync();

        return serviceResult.IsSuccess 
            ? Ok(serviceResult.Data)
            : Problem(serviceResult.ErrorMessage);
    }
    
    [HttpPost("New")]
    public async Task<IActionResult> Create([FromBody] NewModuleAccessRequest request)
    {
        var model = mapper.Map<ModuleAccess>(request);

        var serviceResult = await moduleAccessService.AddModuleAccessAsync(model);
        return serviceResult.IsSuccess 
            ? Ok(serviceResult.Data) 
            : Problem(serviceResult.ErrorMessage);
    }
    
    [HttpPut("Update")]
    public async Task<IActionResult> Update([FromBody] UpdateModuleAccessRequest request)
    {
        var model = mapper.Map<ModuleAccess>(request);

        var serviceResult = await moduleAccessService.UpdateModuleAccessAsync(model);
        return serviceResult.IsSuccess 
            ? Ok(serviceResult.Data) 
            : Problem(serviceResult.ErrorMessage);
    }
    
    [HttpDelete("Delete")]
    public async Task<IActionResult> Delete([FromBody] DeleteModuleAccessRequest request)
    {
        var model = mapper.Map<ModuleAccess>(request);
        
        var serviceResult = await moduleAccessService.DeleteModuleAccessAsync(model);
        return serviceResult.IsSuccess 
            ? Ok() 
            : Problem(serviceResult.ErrorMessage);
    }
    
    [HttpGet("GetById/{moduleAccessId:int}")]
    public async Task<IActionResult> GetModuleAccessById([FromRoute] int moduleAccessId)
    {
        var serviceResult = await moduleAccessService.GetModuleAccessByIdAsync(moduleAccessId);
        
        return serviceResult.IsSuccess 
            ? Ok(serviceResult.Data) 
            : Problem(serviceResult.ErrorMessage);
    }
    
    [HttpGet("GetByCourseId/{courseId:int}")]
    public async Task<IActionResult> GetModuleAccessesByCourseId([FromRoute] int courseId)
    {
        var serviceResult = await moduleAccessService.GetModuleAccessesByCourseIdAsync(courseId);
        
        return serviceResult.IsSuccess 
            ? Ok(serviceResult.Data) 
            : Problem(serviceResult.ErrorMessage);
    }
    
    [HttpGet("GetByModuleId/{moduleId:int}")]
    public async Task<IActionResult> GetModuleAccessesByModuleId([FromRoute] int moduleId)
    {
        var serviceResult = await moduleAccessService.GetModuleAccessesByModuleIdAsync(moduleId);
        
        return serviceResult.IsSuccess 
            ? Ok(serviceResult.Data) 
            : Problem(serviceResult.ErrorMessage);
    }
    
    [HttpGet("GetByUserId/{userId:int}")]
    public async Task<IActionResult> GetModuleAccessesByUserId([FromRoute] int userId)
    {
        var serviceResult = await moduleAccessService.GetModuleAccessesByUserIdAsync(userId);
        
        return serviceResult.IsSuccess 
            ? Ok(serviceResult.Data) 
            : Problem(serviceResult.ErrorMessage);
    }
    
    [HttpGet("GetByUserIdAndCourseId/{userId:int}/{courseId:int}")]
    public async Task<IActionResult> GetModuleAccessesForUserByCourseId([FromRoute] int userId, [FromRoute] int courseId)
    {
        var serviceResult = await moduleAccessService.GetModuleAccessesForUserByCourseIdAsync(userId, courseId);
        
        return serviceResult.IsSuccess 
            ? Ok(serviceResult.Data) 
            : Problem(serviceResult.ErrorMessage);
    }
}
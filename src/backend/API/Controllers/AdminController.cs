using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class AdminController(
    IAdminService adminService) : ControllerBase
{
    [HttpGet("course/{courseId:int}")]
    public async Task<IActionResult> GetUsersByCourseIdAsync(int courseId)
    {
        var serviceResult = await adminService.GetUsersByCourse(courseId);

        return serviceResult.IsSuccess 
            ? Ok(serviceResult.Data)
            : Problem(serviceResult.ErrorMessage);
    }
    
    [HttpPost("course/{courseId:int}/register/{userId:int}")]
    public async Task<IActionResult> AddUserToCourse(int courseId, int userId)
    {
        var serviceResult = await adminService.RegisterUserOnCourse(userId, courseId);
        
        return serviceResult.IsSuccess 
            ? Ok(serviceResult)
            : Problem(serviceResult.ErrorMessage);
    }
}
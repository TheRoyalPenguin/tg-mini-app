using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class AdminController(
    IEnrollmentService enrollmentService) : ControllerBase
{
    [HttpGet("course/{courseId:int}")]
    public async Task<IActionResult> GetUsersByCourseIdAsync(int courseId)
    {
        var serviceResult = await enrollmentService.GetUsersByCourseId(courseId);

        return serviceResult.IsSuccess 
            ? Ok(serviceResult.Data)
            : Problem(serviceResult.ErrorMessage);
    }
}
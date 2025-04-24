using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoursesController : ControllerBase
{
    private readonly ICourseService courseService;

    public CoursesController(ICourseService courseService)
    {
        this.courseService = courseService;
    }

    [HttpGet("available")]
    public async Task<IActionResult> GetAllCourses()
    {
        var result = await courseService.GetAllAsync();

        if (result.IsSuccess)
        {
            return Ok(result.Data);
        }

        return BadRequest(result.ErrorMessage);
    }
}
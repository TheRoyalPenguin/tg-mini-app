using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoursesController : ControllerBase
{
    private readonly IEnrollmentService service;
    public CoursesController(IEnrollmentService enrollmentService)
    {
        this.service = enrollmentService;
    }

    [HttpGet("availablecourses")]
    public async Task<ActionResult<List<string>>> Get([FromQuery]int userId)
    {
        var titles =  await service.GetCourseTitlesByUserId(userId);
        return Ok(titles);
    }
}
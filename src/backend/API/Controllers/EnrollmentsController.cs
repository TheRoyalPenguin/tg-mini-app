using Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EnrollmentsController : ControllerBase
{
    private readonly IEnrollmentService enrollmentService;
    public EnrollmentsController(IEnrollmentService enrollmentService)
    {
        this.enrollmentService = enrollmentService;
    }

    [Authorize]
    [HttpGet("availablecourses")]
    public async Task<ActionResult<ICollection<string>>> GetEnrollments()
    {
        throw new ArgumentException();
    }
}
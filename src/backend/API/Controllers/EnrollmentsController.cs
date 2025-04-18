using System.Security.Claims;
using API.DTO.Enrollment;
using AutoMapper;
using Core.Interfaces.Services;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EnrollmentsController : ControllerBase
{
    private readonly IEnrollmentService enrollmentService;
    private readonly IMapper mapper;
    public EnrollmentsController(IEnrollmentService enrollmentService, IMapper mapper)
    {
        this.enrollmentService = enrollmentService;
        this.mapper = mapper;
    }

    [Authorize]
    [HttpGet("availablecourses")]
    public async Task<ActionResult<ICollection<string>>> GetAvailableCourses()
    {
        // Получение ID пользователя из JWT
        var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out var userId))
        {
            return Unauthorized("Не удалось определить ID пользователя из токена.");
        }

        var result = await enrollmentService.GetCourseTitlesByUserId(userId);

        return result.IsSuccess ? Ok(result.Data) : Problem(result.ErrorMessage);
    }
    
    [HttpGet("all")]
    public async Task<IActionResult> GetAllEnrollments()
    {
        var result = await enrollmentService.GetAllAsync();
        return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
    }
    
    [HttpGet("GetById/{Id:int}")]
    public async Task<IActionResult> GetModuleById([FromRoute] int Id)
    {
        var serviceResult = await enrollmentService.GetByIdAsync(Id);
        
        return serviceResult.IsSuccess 
            ? Ok(serviceResult.Data) 
            : Problem(serviceResult.ErrorMessage);
    }
    
    [HttpDelete("Delete/{enrollmentId:int}")]
    public async Task<IActionResult> Delete([FromRoute] int enrollmentId)
    {
        var serviceResult = await enrollmentService.DeleteAsync(enrollmentId);
        
        return serviceResult.IsSuccess 
            ? Ok() 
            : Problem(serviceResult.ErrorMessage);
    }

    [HttpPost("new")]
    public async Task<IActionResult> Create([FromBody] CreateEnrollmentDto createEnrollmentDto)
    {
        var enrollment = mapper.Map<Enrollment>(createEnrollmentDto);

        var serviceResult = await enrollmentService.AddAsync(enrollment);
        return serviceResult.IsSuccess 
            ? Ok(serviceResult.Data) 
            : Problem(serviceResult.ErrorMessage);
    }
    
    [HttpPut("update")]
    public async Task<IActionResult> Update([FromBody] UpdateEnrollmentDto request)
    {
        var model = mapper.Map<Enrollment>(request);

        var serviceResult = await enrollmentService.UpdateAsync(model);
        return serviceResult.IsSuccess 
            ? Ok(serviceResult.Data) 
            : Problem(serviceResult.ErrorMessage);
    }
}
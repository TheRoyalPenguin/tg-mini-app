using System.Security.Claims;
using System.Text;
using System.Text.Json;
using API.DTO.Testing;
using AutoMapper;
using Core.Interfaces.Services;
using Core.Models;
using Core.Utils;
using Microsoft.AspNetCore.Mvc;
using Minio.DataModel.Args;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestingController : ControllerBase
{
    private readonly ITestingService testingService;
    private readonly IMapper mapper;
    public TestingController(ITestingService testingService, IMapper mapper)
    {
        this.testingService = testingService;
        this.mapper = mapper;
    }

    [HttpGet("courses/{courseId}/modules/{moduleId}/questions")]
    public async Task<IActionResult> GetQuestionsForTest(
        [FromRoute] int courseId, 
        [FromRoute] int moduleId)
    {
        var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out var userId))
        {
            return Unauthorized("Не удалось определить ID пользователя.");
        }

        if (courseId <= 0 || moduleId <= 0)
            return BadRequest("ID курса и модуля должны быть положительными.");
        
        var result = await testingService.GetQuestionsForTest(courseId, moduleId, userId);

        if (result.IsSuccess)
        {
            var dto = mapper.Map<List<TestingQuestionDto>>(result.Data);
            return Ok(dto);
        }

        return Problem(result.ErrorMessage);
    }
    
    [HttpPost("courses/{courseId}/modules/{moduleId}/submit")]
    public async Task<IActionResult> SubmitAnswers(
        [FromRoute] int courseId,
        [FromRoute] int moduleId,
        [FromBody] SubmitAnswersDto dto)
    {
        var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out var userId))
            return Unauthorized("Не удалось определить ID пользователя");

        var model = mapper.Map<SubmitAnswersCommand>(dto);
        model.ModuleId = moduleId;
        model.UserId = userId;

        var result = await testingService.SubmitAnswers(model);

        return result.IsSuccess 
            ? Ok(mapper.Map<SubmitAnswersResultDto>(result.Data)) 
            : Problem(result.ErrorMessage);
    }
    
    [HttpPost("course/{courseId}/modules/{moduleId}/questions")]
    public async Task<IActionResult> AddOrUpdateTest([FromRoute] int courseId, [FromRoute] int moduleId, [FromBody] List<AddOrUpdateTestQuestions> dto)
    {
        var testQuestions = mapper.Map<List<TestingQuestion>>(dto);
        var result = await testingService.AddOrUpdateTestAsync(courseId, moduleId, testQuestions);

        if (result.IsSuccess)
        {
            return Ok("Тест был успешно добавлен или обновлен.");
        }

        return Problem(result.ErrorMessage);
    }
}
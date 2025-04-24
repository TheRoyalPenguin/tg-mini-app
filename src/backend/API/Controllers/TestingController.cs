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

    [HttpGet("{courseId}/modules/{moduleId}/questions")]
    public async Task<IActionResult> GetQuestionsForTest([FromRoute] int courseId, [FromRoute] int moduleId)
    {
        if (courseId <= 0 || moduleId <= 0)
            return BadRequest("courseId и moduleId должны быть положительными целыми числами.");

        var result = await testingService.GetQuestionsForTest(courseId, moduleId);

        if (result.IsSuccess)
        {
            var dto = mapper.Map<List<TestingQuestionDto>>(result.Data);
            return Ok(dto);
        }

        return Problem(result.ErrorMessage);
    }
    
    [HttpPost("course/{courseId}/submit")]
    public async Task<IActionResult> SubmitAnswers([FromRoute] int courseId, [FromBody] SubmitAnswersDto dto)

    {
        var answers = mapper.Map<SubmitAnswersCommand>(dto);
        var result = await testingService.SubmitAnswers(answers);

        if (result.IsSuccess)
        {
            // Маппим результат в DTO
            var resultDto = mapper.Map<SubmitAnswersResultDto>(result.Data);
            return Ok(resultDto);
        }

        return Problem(result.ErrorMessage);
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
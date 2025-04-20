using API.DTO.Testing;
using AutoMapper;
using Core.Interfaces.Services;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

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

    [HttpGet("{moduleId}/questions")]
    public async Task<IActionResult> GetQuestionsForTest([FromRoute] int moduleId)
    {
        var result = await testingService.GetQuestionsForTest(moduleId);

        if (result.IsSuccess)
        {
            var dto = mapper.Map<List<TestingQuestionDto>>(result.Data);
            return Ok(dto);
        }

        return Problem(result.ErrorMessage);
    }
    
    [HttpPost("submit")]
    public async Task<IActionResult> SubmitAnswers([FromBody] SubmitAnswersDto dto)
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
}
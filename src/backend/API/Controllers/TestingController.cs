using System.Security.Claims;
using System.Text.Json;
using API.DTO.Testing;
using AutoMapper;
using Core.Interfaces.Services;
using Core.Models;
using Core.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/")]
    public class TestingController : ControllerBase
    {
        private readonly ITestingService testingService;
        private readonly IMapper mapper;
        private readonly ILogger<TestingController> _logger;

        public TestingController(ITestingService testingService, IMapper mapper, ILogger<TestingController> logger)
        {
            this.testingService = testingService;
            this.mapper = mapper;
            _logger = logger;
        }

        [HttpGet("courses/{courseId}/modules/{moduleId}/questions")]
        public async Task<IActionResult> GetQuestionsForTest(
            [FromRoute] int courseId, 
            [FromRoute] int moduleId)
        {
            _logger.LogInformation("GetQuestionsForTest called for courseId: {courseId}, moduleId: {moduleId}", courseId, moduleId);

            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out var userId))
            {
                _logger.LogWarning("User ID not found or invalid");
                return Unauthorized("Не удалось определить ID пользователя.");
            }

            if (courseId <= 0 || moduleId <= 0)
            {
                _logger.LogWarning("Invalid courseId or moduleId: courseId={courseId}, moduleId={moduleId}", courseId, moduleId);
                return BadRequest("ID курса и модуля должны быть положительными.");
            }

            var result = await testingService.GetQuestionsForTest(courseId, moduleId, userId);

            if (result.IsSuccess)
            {
                var dto = mapper.Map<List<TestingQuestionDto>>(result.Data);
                _logger.LogInformation("Successfully retrieved questions for courseId: {courseId}, moduleId: {moduleId}", courseId, moduleId);
                return Ok(dto);
            }

            _logger.LogError("Failed to retrieve questions for courseId: {courseId}, moduleId: {moduleId}, Error: {ErrorMessage}", courseId, moduleId, result.ErrorMessage);
            return Problem(result.ErrorMessage);
        }
        
        [HttpPost("courses/{courseId}/modules/{moduleId}/submit")]
        public async Task<IActionResult> SubmitAnswers(
            [FromRoute] int courseId,
            [FromRoute] int moduleId,
            [FromBody] SubmitAnswersDto dto)
        {
            _logger.LogInformation("SubmitAnswers called for courseId: {courseId}, moduleId: {moduleId}", courseId, moduleId);

            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out var userId))
            {
                _logger.LogWarning("User ID not found or invalid");
                return Unauthorized("Не удалось определить ID пользователя");
            }

            var model = mapper.Map<SubmitAnswersCommand>(dto);
            model.ModuleId = moduleId;
            model.UserId = userId;
            model.CourseId = courseId;

            var result = await testingService.SubmitAnswers(model);

            if (result.IsSuccess)
            {
                _logger.LogInformation("Successfully submitted answers for courseId: {courseId}, moduleId: {moduleId}, userId: {userId}", courseId, moduleId, userId);
                return Ok(mapper.Map<SubmitAnswersResultDto>(result.Data));
            }

            _logger.LogError("Failed to submit answers for courseId: {courseId}, moduleId: {moduleId}, userId: {userId}, Error: {ErrorMessage}", courseId, moduleId, userId, result.ErrorMessage);
            return Problem(result.ErrorMessage);
        }
        
        [HttpPost("course/{courseId}/modules/{moduleId}/questions")]
        public async Task<IActionResult> AddOrUpdateTest([FromRoute] int courseId, [FromRoute] int moduleId, [FromBody] List<AddOrUpdateTestQuestions> dto)
        {
            _logger.LogInformation("AddOrUpdateTest called for courseId: {courseId}, moduleId: {moduleId}", courseId, moduleId);

            var testQuestions = mapper.Map<List<TestingQuestion>>(dto);
            var result = await testingService.AddOrUpdateTestAsync(courseId, moduleId, testQuestions);

            if (result.IsSuccess)
            {
                _logger.LogInformation("Successfully added or updated test for courseId: {courseId}, moduleId: {moduleId}", courseId, moduleId);
                return Ok("Тест был успешно добавлен или обновлен.");
            }

            _logger.LogError("Failed to add or update test for courseId: {courseId}, moduleId: {moduleId}, Error: {ErrorMessage}", courseId, moduleId, result.ErrorMessage);
            return Problem(result.ErrorMessage);
        }
    }
}

using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestingController : ControllerBase
{
    private readonly ITestingService testingService;
    public TestingController(ITestingService testingService)
    {
        this.testingService = testingService;
    }

    [HttpGet("{moduleId}/questions")]
    public async Task<IActionResult> GetQuestionsForTest([FromRoute] int moduleId)
    {
        var result = await testingService.GetQuestionsForTest(moduleId);

        return result.IsSuccess ? Ok(result.Data) : Problem(result.ErrorMessage);
    }
}
using Core.Interfaces.Services;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/modules/{moduleId}/longreads")]
public class LongreadsController : ControllerBase
{
    private readonly ILongreadService _service;
    public LongreadsController(ILongreadService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Longread>>> ListByModule(
        [FromRoute] int moduleId,
        CancellationToken ct)
    {
        var itemsResult = await _service.ListByModuleAsync(moduleId, ct);
        return itemsResult.IsSuccess
            ? Ok(itemsResult.Data)
            : Problem(itemsResult.ErrorMessage);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Longread>> GetById(
        [FromRoute] int id,
        CancellationToken ct)
    {
        var itemResult = await _service.GetByIdAsync(id, ct);
        return itemResult.IsSuccess
            ? Ok(itemResult.Data)
            : Problem(itemResult.ErrorMessage);
    }
}

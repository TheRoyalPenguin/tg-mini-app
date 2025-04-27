using API.DTO.Longreads;
using Core.Interfaces.Services;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api")]
public class LongreadsController : ControllerBase
{
    private readonly ILongreadService _service;
    private readonly IStorageService _storage;
    public LongreadsController(ILongreadService service, IStorageService storage)
    {
        _service = service;
        _storage = storage;
    }

    [HttpGet("modules/{moduleId}/longreads")]
    public async Task<ActionResult<IReadOnlyList<Longread>>> ListByModule(
        [FromRoute] int moduleId,
        CancellationToken ct)
    {
        var itemsResult = await _service.ListByModuleAsync(moduleId, ct);
        return itemsResult.IsSuccess
            ? Ok(itemsResult.Data)
            : Problem(itemsResult.ErrorMessage);
    }

    [HttpGet("longreads/{id}")]
    public async Task<ActionResult<ResponseLongreadDto>> GetById(
        [FromRoute] int id,
        CancellationToken ct)
    {
        var result = await _service.GetByIdAsync(id, ct);
        if (!result.IsSuccess)
            return Problem(result.ErrorMessage);

        var lr = result.Data;

        var dto = new ResponseLongreadDto
        {
            Id = lr.Id,
            Title = lr.Title,
            Description = lr.Description,
            ModuleId = lr.ModuleId,
            HtmlUrl = await _storage.GetPresignedUrlAsync(lr.HtmlContentKey),
            OriginalDocxUrl = await _storage.GetPresignedUrlAsync(lr.OriginalDocxKey),
            ImageUrls = await lr.ImageKeys
                                  .ToAsyncEnumerable()
                                  .SelectAwait(async key => await _storage.GetPresignedUrlAsync(key))
                                  .ToListAsync()
        };

        return Ok(dto);
    }
}

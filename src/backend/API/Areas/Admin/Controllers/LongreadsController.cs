using API.DTO.Longreads;
using AutoMapper;
using Core.Interfaces.Services;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Areas.Admin.Controllers;

[ApiController]
[Area("Admin")]
[Route("api/admin")]
public class LongreadsController : ControllerBase
{
    private readonly ILongreadService _service;
    private readonly IMapper _mapper;

    public LongreadsController(ILongreadService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
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
    public async Task<ActionResult<Longread>> GetById(
        [FromRoute] int id,
        CancellationToken ct)
    {
        var itemResult = await _service.GetByIdAsync(id, ct);
        return itemResult.IsSuccess
            ? Ok(itemResult.Data)
            : Problem(itemResult.ErrorMessage);
    }

    [HttpPost("modules/{moduleId}/longreads")]
    public async Task<ActionResult<Longread>> Create(
        [FromRoute] int moduleId,
        [FromForm] CreateLongreadDto dto,
        CancellationToken ct)
    {
        var cmd = new CreateLongreadModel
        {
            ModuleId = moduleId,
            Title = dto.Title,
            Description = dto.Description,

            DocxStream = dto.DocxFile.OpenReadStream(),
            DocxFileName = dto.DocxFile.FileName,
            AudioStream = dto.AudioFile?.OpenReadStream(),
            AudioFileName = dto.AudioFile?.FileName
        };

        var result = await _service.AddAsync(moduleId, cmd, ct);
        if (!result.IsSuccess)
        {
            return Problem(result.ErrorMessage);
        }

        var newId = result.Data;
        return CreatedAtAction(nameof(GetById),
                               new { moduleId, id = newId },
                               null);
    }

    [HttpPut("longreads")]
    public async Task<IActionResult> Update(
        [FromBody] UpdateLongreadDto dto,
        CancellationToken ct)
    {
        var longread = _mapper.Map<Longread>(dto);

        await _service.UpdateAsync(longread, ct);
        return NoContent();
    }

    [HttpDelete("longreads/{id}")]
    public async Task<IActionResult> Delete(
        [FromRoute] int id,
        CancellationToken ct)
    {
        await _service.DeleteAsync(id, ct);
        return NoContent();
    }
}

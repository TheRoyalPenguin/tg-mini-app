using API.DTO.Book;
using Core.Interfaces.Services;
using Core.Models;
using Core.Utils;
using Microsoft.AspNetCore.Mvc;

namespace API.Areas.Admin.Controllers;

[ApiController]
[Area("Admin")]
[Route("api/admin")]
public class BooksController : ControllerBase
{
    private readonly IBookService _service;
    private readonly IStorageService _storage;
    public BooksController(IBookService service, IStorageService storage)
    {
        _service = service;
        _storage = storage;
    }

    [HttpGet("modules/{moduleId}/books")]
    public async Task<ActionResult<IReadOnlyList<Book>>> ListByModule(
        [FromRoute] int moduleId,
        CancellationToken ct)
    {
        var listResult = await _service.ListByModuleAsync(moduleId, ct);
        if (!listResult.IsSuccess)
            return Problem(listResult.ErrorMessage);

        var books = listResult.Data;
        if (books == null || books.Count == 0)
            return NotFound("Книги в модуле id=" + moduleId + " не найдены.");

        var tasks = books.Select(async book => new ResponseBookDto
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author,
            ContentUrl = await _storage.GetPresignedUrlAsync(book.ContentKey),
            CoverUrl = string.IsNullOrEmpty(book.CoverKey) ? null : await _storage.GetPresignedUrlAsync(book.CoverKey)
        });

        var dtos = await Task.WhenAll(tasks);
        return Ok(dtos);
    }

    [HttpGet("books/{id}")]
    public async Task<ActionResult<Book>> GetById(
        [FromRoute] int id,
        CancellationToken ct)
    {
        var getResult = await _service.GetByIdAsync(id, ct);
        if (!getResult.IsSuccess)
            return Problem(getResult.ErrorMessage);

        var book = getResult.Data;
        if (book == null)
            return NotFound($"Книга id={id} не найдена.");

        var dto = new ResponseBookDto
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author,
            ContentUrl = await _storage.GetPresignedUrlAsync(book.ContentKey),
            CoverUrl = string.IsNullOrEmpty(book.CoverKey)
                   ? null
                   : await _storage.GetPresignedUrlAsync(book.CoverKey)
        };


        return Ok(dto);
    }

    [HttpPost("course/{courseId}/modules/{moduleId}/books")]
    public async Task<ActionResult<Book>> Create(
        [FromRoute] int courseId,
        [FromRoute] int moduleId,
        [FromForm] CreateBookDto dto,
        CancellationToken ct)
    {
        var cbm = new CreateBookModel
        {
            CourseId = courseId,
            ModuleId = moduleId,
            Title = dto.Title,
            Author = dto.Author,

            ContentStream = dto.ContentFile.OpenReadStream(),
            ContentName = dto.ContentFile.FileName,
            CoverStream = dto.CoverFile?.OpenReadStream(),
            CoverName = dto.CoverFile?.FileName
        };

        var addResult = await _service.AddAsync(cbm, ct);
        if (!addResult.IsSuccess)
            return Problem(addResult.ErrorMessage);

        var createdBookId = addResult.Data;
        return CreatedAtAction(nameof(GetById),
            new { courseId, moduleId, id = createdBookId },
            new { id = createdBookId }
            );
    }

    [HttpPut("courses/{courseId}/modules/{moduleId}/books/{id}")]
    public async Task<IActionResult> Update(
        [FromRoute] int courseId,
        [FromRoute] int moduleId,
        [FromRoute] int id,
        [FromForm] UpdateBookDto dto,
        CancellationToken ct)
    {
        var ubm = new UpdateBookModel
        {
            Id = id,
            CourseId = courseId,
            ModuleId = moduleId,
            Title = dto.Title,
            Author = dto.Author,
            NewContentStream = dto.ContentFile?.OpenReadStream(),
            NewContentName = dto.ContentFile?.FileName,
            NewCoverStream = dto.CoverFile?.OpenReadStream(),
            NewCoverName = dto.CoverFile?.FileName
        };

        var updResult = await _service.UpdateAsync(ubm, ct);
        if (!updResult.IsSuccess)
            return Problem(updResult.ErrorMessage);

        return NoContent();
    }

    [HttpDelete("books/{id}")]
    public async Task<IActionResult> Delete(
        [FromRoute] int id,
        CancellationToken ct)
    {
        var delResult = await _service.DeleteAsync(id, ct);
        if (!delResult.IsSuccess)
            return Problem(delResult.ErrorMessage);

        return NoContent();
    }
}

using API.DTO.Book;
using API.DTO.Longreads;
using Core.Interfaces.Services;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/modules/{moduleId}/books")]
public class BooksController : ControllerBase
{
    private readonly IBookService _service;
    private readonly IStorageService _storage;
    public BooksController(IBookService service, IStorageService storage)
    {
        _service = service;
        _storage = storage;
    }

    [HttpGet]
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
}

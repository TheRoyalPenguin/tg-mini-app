namespace API.DTO.Book;

public class UpdateBookDto
{
    public string Title { get; set; } = default!;
    public string Author { get; set; } = default!;
    public IFormFile? ContentFile { get; set; }
    public IFormFile? CoverFile { get; set; }
}

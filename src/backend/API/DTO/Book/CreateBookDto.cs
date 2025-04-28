namespace API.DTO.Book;

public class CreateBookDto
{
    public string Title { get; set; } = null!;
    public string Author { get; set; } = null!;
    public IFormFile ContentFile { get; set; } = null!;
    public IFormFile? CoverFile { get; set; }
}

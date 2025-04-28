namespace API.DTO.Book;

public class ResponseBookDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Author { get; set; } = null!;
    public string ContentUrl { get; set; } = null!;
    public string? CoverUrl { get; set; }
}

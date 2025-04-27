namespace Core.Models;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Author { get; set; } = null!;
    public string FileKey { get; set; } = null!;
    public string? CoverKey { get; set; }

    public List<int> ModuleIds { get; set; } = new();
}

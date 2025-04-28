namespace Core.Models;

public class UpdateBookModel
{
    public int Id { get; set; }
    public int CourseId { get; set; }
    public int ModuleId { get; set; }
    public string Title { get; set; } = null!;
    public string Author { get; set; } = null!;
    public Stream? NewContentStream { get; set; }
    public string? NewContentName { get; set; }
    public Stream? NewCoverStream { get; set; }
    public string? NewCoverName { get; set; }
}

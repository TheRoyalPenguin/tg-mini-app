namespace Core.Models;

public class CreateBookModel
{
    public int CourseId { get; init; }
    public int ModuleId { get; init; }
    public string Title { get; init; } = null!;
    public string Author { get; init; } = null!;

    public Stream ContentStream { get; init; } = null!;
    public string ContentName { get; init; } = null!;

    public Stream? CoverStream { get; init; }
    public string? CoverName { get; init; }
}

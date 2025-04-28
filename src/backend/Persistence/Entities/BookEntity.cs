namespace Persistence.Entities;

public class BookEntity
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Author { get; set; } = null!;
    public string ContentKey { get; set; } = null!;
    public string? CoverKey { get; set; }

    public int ModuleId { get; set; }
    public ModuleEntity Module { get; set; } = null!;
}

namespace Persistence.Entities;

public class BookEntity
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Author { get; set; } = null!;
    public string FileKey { get; set; } = null!;
    public string? CoverKey { get; set; }
    public ICollection<ModuleBookEntity> ModuleBooks { get; set; } = new List<ModuleBookEntity>();
}


namespace Persistence.Entities;

public class ModuleBookEntity
{
    public int ModuleId { get; set; }
    public ModuleEntity Module { get; set; } = null!;
    public int BookId { get; set; }
    public BookEntity Book { get; set; } = null!;
}

namespace Core.Models;

public class ModuleWithAccess
{
    public int Id { get; set; }
    public required string Title { get; set; } = null!;
    public required bool IsAccessed { get; set; }
}

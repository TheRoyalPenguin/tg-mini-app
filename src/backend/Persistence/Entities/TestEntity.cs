
using Persistence.Entities;

public class TestEntity
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public string JsonKey { get; set; } = null!;

    public int ModuleId { get; set; }
    public ModuleEntity Module { get; set; } = null!;
}

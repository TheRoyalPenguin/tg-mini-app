namespace Persistence.Entities;

public class LessonEntity
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    
    public required int ModuleId { get; set; }
    public ModuleEntity Module { get; set; }
}
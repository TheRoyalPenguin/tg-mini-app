namespace Core.Models;

public class Lesson
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required int ModuleId { get; set; }
}
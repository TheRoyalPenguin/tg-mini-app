namespace Core.Models;

public class Module
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required int CourseId { get; set; }
}
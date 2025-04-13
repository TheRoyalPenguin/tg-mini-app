namespace Core.Models;

public class Resource
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required int LessonId { get; set; }
}
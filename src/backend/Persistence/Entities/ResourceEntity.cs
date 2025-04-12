namespace Persistence.Entities;

public class ResourceEntity
{
    public required int Id { get; set; }
    public required string Title { get; set; }
    
    public required int LessonId { get; set; }
    public LessonEntity Lesson { get; set; }
}
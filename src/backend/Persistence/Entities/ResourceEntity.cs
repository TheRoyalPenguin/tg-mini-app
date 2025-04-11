namespace Persistence.Entities;

public class ResourceEntity
{
    public required Guid Id { get; set; }
    public required string Title { get; set; }
    
    public required Guid LessonId { get; set; }
    public LessonEntity Lesson { get; set; }
}
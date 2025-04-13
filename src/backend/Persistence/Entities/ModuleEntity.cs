namespace Persistence.Entities;

public class ModuleEntity
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    
    public required int CourseId { get; set; }
    public CourseEntity Course { get; set; }
    
    public List<LessonEntity> Lessons { get; set; }
}
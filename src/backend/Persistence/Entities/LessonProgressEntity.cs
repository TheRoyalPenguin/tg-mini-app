namespace Persistence.Entities;

public class LessonProgressEntity
{
    public required Guid Id { get; set; }
    public required bool IsLessonCompleted { get; set; }
    public required DateOnly LastActivityDate { get; set; }
    
    public required Guid UserId { get; set; }
    public UserEntity User { get; set; }

    public required Guid LessonId { get; set; }
    public LessonEntity Lesson { get; set; }
}
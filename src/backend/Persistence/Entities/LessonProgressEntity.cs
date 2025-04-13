namespace Persistence.Entities;

public class LessonProgressEntity
{
    public int Id { get; set; }
    public required bool IsLessonCompleted { get; set; }
    public required DateOnly LastActivityDate { get; set; }
    
    public required int UserId { get; set; }
    public UserEntity User { get; set; }

    public required int LessonId { get; set; }
    public LessonEntity Lesson { get; set; }
}
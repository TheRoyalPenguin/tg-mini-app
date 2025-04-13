namespace Core.Models;

public class LessonProgress
{
    public int Id { get; set; }
    public required bool IsLessonCompleted { get; set; }
    public required DateOnly LastActivityDate { get; set; }
    public required int UserId { get; set; }
    public required int LessonId { get; set; }
}
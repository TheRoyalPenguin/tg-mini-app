using EfCore.Conventions.Attributes;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Entities;

public class LessonProgressEntity
{
    public int Id { get; set; }
    public required bool IsLessonCompleted { get; set; }
    public required DateOnly LastActivityDate { get; set; }
    
    [OnDelete(DeleteBehavior.Cascade)]
    public required int UserId { get; set; }
    public UserEntity User { get; set; }

    [OnDelete(DeleteBehavior.Cascade)]
    public required int LessonId { get; set; }
    public LessonEntity Lesson { get; set; }
}
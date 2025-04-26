using EfCore.Conventions.Attributes;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Entities;

public class EnrollmentEntity
{
    public int Id { get; set; }
    public bool IsCourseCompleted { get; set; }
    
    public DateOnly EnrollmentDate { get; set; }
    public DateOnly CompletionDate { get; set; }
    
    [OnDelete(DeleteBehavior.Cascade)]
    public int UserId { get; set; }
    public UserEntity User { get; set; } = null!;

    [OnDelete(DeleteBehavior.Cascade)]
    public int CourseId { get; set; }
    public CourseEntity Course { get; set; } = null!;
}
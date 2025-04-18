using EfCore.Conventions.Attributes;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Entities;

public class EnrollmentEntity
{
    public int Id { get; set; }
    public required bool IsCourseCompleted { get; set; }
    
    public required DateOnly EnrollmentDate { get; set; }
    public DateOnly CompletionDate { get; set; }
    
    [OnDelete(DeleteBehavior.Cascade)]
    public required int UserId { get; set; }
    public UserEntity User { get; set; }

    [OnDelete(DeleteBehavior.Cascade)]
    public required int CourseId { get; set; }
    public CourseEntity Course { get; set; }
}
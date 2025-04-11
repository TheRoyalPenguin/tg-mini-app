namespace Persistence.Entities;

public class EnrollmentEntity
{
    public required Guid Id { get; set; }
    public required bool IsCourseCompleted { get; set; }
    
    public required DateOnly EnrollmentDate { get; set; }
    public DateOnly CompletionDate { get; set; }
    
    public required Guid UserId { get; set; }
    public UserEntity User { get; set; }
    
    public required Guid CourseId { get; set; }
    public CourseEntity Course { get; set; }
}
namespace Core.Models;

public class Enrollment
{
    public int Id { get; set; }
    public required bool IsCourseCompleted { get; set; }
    public required DateOnly EnrollmentDate { get; set; }
    public DateOnly? CompletionDate { get; set; }
    public required int UserId { get; set; }
    public required int CourseId { get; set; }
}
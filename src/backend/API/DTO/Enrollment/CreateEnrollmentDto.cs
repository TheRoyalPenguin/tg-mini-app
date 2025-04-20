namespace API.DTO.Enrollment;

public record CreateEnrollmentDto(
    bool IsCourseCompleted,
    DateOnly EnrollmentDate,
    DateOnly? CompletionDate,
    int UserId,
    int CourseId);
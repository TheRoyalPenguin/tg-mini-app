namespace API.DTO.Enrollment;

public record class UpdateEnrollmentDto(
    int Id,
    bool IsCourseCompleted,
    DateOnly EnrollmentDate,
    DateOnly? CompletionDate,
    int UserId,
    int CourseId
);

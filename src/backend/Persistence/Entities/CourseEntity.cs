namespace Persistence.Entities;

public class CourseEntity
{
    public required int Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }

    public List<ModuleEntity> Modules { get; set; } = [];
    public List<EnrollmentEntity> Enrollments { get; set; } = [];
}
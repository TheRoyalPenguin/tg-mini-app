namespace Persistence.Entities;

public class CourseEntity
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;

    public List<ModuleEntity> Modules { get; set; } = [];
    public List<EnrollmentEntity> Enrollments { get; set; } = [];
}
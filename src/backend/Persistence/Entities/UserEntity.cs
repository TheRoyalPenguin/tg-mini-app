namespace Persistence.Entities;

public class UserEntity
{
    public required int Id { get; set; }
    public required string Login { get; set; }
    
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public required string Patronymic { get; set; }
    public required string PhoneNumber { get; set; }
    public required bool IsBanned { get; set; } = false;
    
    public required int RoleId { get; set; }
    public RoleEntity Role { get; set; }
    
    public List<CourseEntity> Courses { get; set; }
    public List<EnrollmentEntity> Enrollments { get; set; } = [];
    
    public List<ModuleEntity> Modules { get; set; } = [];
    public List<ModuleAccessEntity> ModuleAccesses { get; set; } = [];
    
    public List<LessonEntity> Lessons { get; set; } = [];
    public List<LessonProgressEntity> LessonsProgress { get; set; } = [];
}
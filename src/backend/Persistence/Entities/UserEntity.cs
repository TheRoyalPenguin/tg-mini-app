using EfCore.Conventions.Attributes;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Entities;

public class UserEntity
{
    public int Id { get; set; }
    public long TgId { get; set; }
    
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string Patronymic { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public bool IsBanned { get; set; } = false;
    public DateTime RegisteredAt { get; set; }
    public int? NotificationDaysLimit { get; set; }

    [OnDelete(DeleteBehavior.Restrict)]
    public int RoleId { get; set; }
    public RoleEntity Role { get; set; } = null!;

    public List<EnrollmentEntity> Enrollments { get; set; } = [];
    public List<ModuleAccessEntity> ModuleAccesses { get; set; } = [];
}
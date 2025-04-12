using Microsoft.EntityFrameworkCore;
using Persistence.Configurations;
using Persistence.Entities;

namespace Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<CourseEntity> Courses { get; set; }
    public DbSet<ModuleEntity> Modules { get; set; }
    public DbSet<LessonEntity> Lessons { get; set; }
    public DbSet<EnrollmentEntity> Enrollments { get; set; }
    public DbSet<ModuleAccessEntity> ModuleAccesses { get; set; }
    public DbSet<LessonProgressEntity> LessonProgresses { get; set; }
    public DbSet<RoleEntity> Roles { get; set; }
    public DbSet<ResourceEntity> Resources { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new CourseConfiguration());
        modelBuilder.ApplyConfiguration(new ModuleConfiguration());
        modelBuilder.ApplyConfiguration(new LessonConfiguration());
        modelBuilder.ApplyConfiguration(new EnrollmentConfiguration());
        modelBuilder.ApplyConfiguration(new ModuleAccessConfiguration());
        modelBuilder.ApplyConfiguration(new LessonProgressConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new ResourceConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
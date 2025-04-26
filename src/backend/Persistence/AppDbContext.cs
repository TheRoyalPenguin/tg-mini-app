using Microsoft.EntityFrameworkCore;
using Persistence.Entities;

namespace Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<CourseEntity> Courses { get; set; }
    public DbSet<ModuleEntity> Modules { get; set; }
    public DbSet<EnrollmentEntity> Enrollments { get; set; }
    public DbSet<ModuleAccessEntity> ModuleAccesses { get; set; }
    public DbSet<RoleEntity> Roles { get; set; }
    public DbSet<ResourceEntity> Resources { get; set; }
    public DbSet<LongreadCompletionEntity> LongreadCompletions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
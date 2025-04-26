using EfCore.Conventions.Attributes;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Entities;

public class ModuleEntity
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int LongreadCount { get; set; }

    [OnDelete(DeleteBehavior.Cascade)]
    public int CourseId { get; set; }
    public CourseEntity Course { get; set; } = null!;

    public ICollection<LongreadEntity> Longreads { get; set; } = new List<LongreadEntity>();
    public ICollection<TestEntity> Tests { get; set; } = new List<TestEntity>();
    public ICollection<ModuleBookEntity> ModuleBooks { get; set; } = new List<ModuleBookEntity>();
}
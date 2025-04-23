using EfCore.Conventions.Attributes;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Entities;

public class ModuleEntity
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required int LongreadCount { get; set; }
    
    [OnDelete(DeleteBehavior.Cascade)]
    public required int CourseId { get; set; }
    public CourseEntity Course { get; set; }

    public List<ResourceEntity> Resources { get; set; } = [];
}
using EfCore.Conventions.Attributes;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Entities;

public class ResourceEntity
{
    public int Id { get; set; }
    public required string Type { get; set; }
    public required string Json { get; set; }
    
    [OnDelete(DeleteBehavior.Restrict)]
    public required int ModuleId { get; set; }
    public ModuleEntity Module { get; set; }
}
using EfCore.Conventions.Attributes;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Entities;

public class LessonEntity
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    
    [OnDelete(DeleteBehavior.Cascade)]
    public required int ModuleId { get; set; }
    public ModuleEntity Module { get; set; }
}
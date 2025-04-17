using EfCore.Conventions.Attributes;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Entities;

public class ModuleAccessEntity
{
    public int Id { get; set; }
    public required bool IsModuleCompleted { get; set; }
    public DateOnly CompletionDate { get; set; }
    
    [OnDelete(DeleteBehavior.Cascade)]
    public required int UserId { get; set; }
    public UserEntity User { get; set; }
    
    [OnDelete(DeleteBehavior.Cascade)]
    public required int ModuleId { get; set; }
    public ModuleEntity Module { get; set; }
}
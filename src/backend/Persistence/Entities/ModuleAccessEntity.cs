using System.Runtime.CompilerServices;
using EfCore.Conventions.Attributes;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Entities;

public class ModuleAccessEntity
{
    public int Id { get; set; }
    
    public int TestTriesCount { get; set; }
    public bool IsModuleCompleted { get; set; }
    public bool IsModuleAvailable { get; set; }
    public DateOnly? CompletionDate { get; set; }
    
    [OnDelete(DeleteBehavior.Cascade)]
    public int UserId { get; set; }
    public UserEntity User { get; set; }
    
    [OnDelete(DeleteBehavior.Cascade)]
    public int ModuleId { get; set; }
    public ModuleEntity Module { get; set; }

    public List<LongreadCompletionEntity> LongreadCompletions { get; set; } = [];   
}
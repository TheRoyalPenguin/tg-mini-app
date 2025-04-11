namespace Persistence.Entities;

public class ModuleAccessEntity
{
    public required Guid Id { get; set; }
    public required bool IsModuleCompleted { get; set; }
    public DateOnly CompletionDate { get; set; }
    
    public required Guid UserId { get; set; }
    public UserEntity User { get; set; }
    
    public required Guid ModuleId { get; set; }
    public ModuleEntity Module { get; set; }
}
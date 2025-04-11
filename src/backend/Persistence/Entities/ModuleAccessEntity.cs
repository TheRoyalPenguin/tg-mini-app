namespace Persistence.Entities;

public class ModuleAccessEntity
{
    public required int Id { get; set; }
    public required bool IsModuleCompleted { get; set; }
    public DateOnly CompletionDate { get; set; }
    
    public required int UserId { get; set; }
    public UserEntity User { get; set; }
    
    public required int ModuleId { get; set; }
    public ModuleEntity Module { get; set; }
}
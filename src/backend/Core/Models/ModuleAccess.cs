namespace Core.Models;

public class ModuleAccess
{
    public int Id { get; set; }
    public required bool IsModuleCompleted { get; set; }
    public DateOnly? CompletionDate { get; set; }
    public required int UserId { get; set; }
    public required int ModuleId { get; set; }
}
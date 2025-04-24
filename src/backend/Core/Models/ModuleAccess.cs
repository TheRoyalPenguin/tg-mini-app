namespace Core.Models;

public class ModuleAccess
{
    public int Id { get; set; }
    public int CompletedLongreadsCount {get; set;}
    public int ModuleLongreadCount {get; set;}
    public bool IsModuleCompleted { get; set; }
    public bool IsModuleAvailable { get; set; }
    public DateOnly? CompletionDate { get; set; }
    public required int UserId { get; set; }
    public required int ModuleId { get; set; }
    public Module? Module { get; set; } 
    public List<LongreadCompletion> LongreadCompletions { get; set; } = [];
}
namespace API.DTO.ModuleAccess;

public class ModuleAccessWithProgress
{
    public int Id { get; set; }
    public int CompletedLongreadsCount {get; set;}
    public int CompletionRate  {get; set;}
    public bool IsModuleCompleted { get; set; }
    public DateOnly? CompletionDate { get; set; }
    public required int UserId { get; set; }
    public required int ModuleId { get; set; }
}
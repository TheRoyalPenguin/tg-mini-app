namespace API.DTO.ModuleAccessRequests;

public class UpdateModuleAccessRequest
{
    public required int Id { get; set; }
    public required int UserId { get; set; }
    public required int ModuleId { get; set; }
    public bool IsModuleCompleted { get; set; }
    public DateOnly? CompletionDate { get; set; }
}
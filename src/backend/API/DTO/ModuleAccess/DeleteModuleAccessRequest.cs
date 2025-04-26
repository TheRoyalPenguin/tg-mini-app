namespace API.DTO.ModuleAccessRequests;

public class DeleteModuleAccessRequest
{
    public required int UserId { get; set; }
    public required int ModuleId { get; set; }
}
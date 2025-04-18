namespace API.DTO.ModuleAccessRequests;

public class NewModuleAccessRequest
{
    public required int UserId { get; set; }
    public required int ModuleId { get; set; }
}
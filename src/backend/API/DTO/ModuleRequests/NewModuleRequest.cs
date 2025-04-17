namespace API.DTO.ModuleRequests;

public class NewModuleRequest
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required int CourseId { get; set; }
}
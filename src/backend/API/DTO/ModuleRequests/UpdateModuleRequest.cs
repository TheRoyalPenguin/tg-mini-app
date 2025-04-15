namespace API.DTO;

public class UpdateModuleRequest
{
    public required int Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required int CourseId { get; set; }
}
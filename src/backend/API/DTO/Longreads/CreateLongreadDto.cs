namespace API.DTO.Longreads;

public class CreateLongreadDto
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public IFormFile DocxFile { get; set; } = null!;
    public IFormFile? AudioFile { get; set; }
}

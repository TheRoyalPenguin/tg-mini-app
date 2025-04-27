namespace API.DTO;

public class UpdateLongreadDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public IFormFile DocxFile { get; set; } = null!;
}

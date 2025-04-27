namespace API.DTO.Longreads;

public class ResponseLongreadDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string HtmlUrl { get; set; } = null!;
    public string? AudioUrl { get; set; }
    public List<string> ImageUrls { get; set; } = new();
    public string OriginalDocxUrl { get; set; } = null!;
    public int ModuleId { get; set; }
}

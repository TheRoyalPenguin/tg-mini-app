namespace Core.Models;

public class Resource
{
    public int Id { get; set; }
    public required ResourceType Type { get; set; }
    public required string JsonUri { get; set; }
    public required int ModuleId { get; set; }
}

public enum ResourceType
{
    Longread,
    Test,
    BookRecommendation
}
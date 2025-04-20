namespace Core.Models;

public class TestingQuestion
{
    public required string Question { get; set; }
    public List<string> Options { get; set; } = [];
}
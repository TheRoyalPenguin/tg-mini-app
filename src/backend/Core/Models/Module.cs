namespace Core.Models;

public class Module
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    
    public int CourseId { get; set; }
}
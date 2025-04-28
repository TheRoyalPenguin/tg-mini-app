namespace Core.Models;

public class SubmitAnswersCommand
{
    public int UserId { get; set; }
    public int CourseId { get; set; }
    public int ModuleId { get; set; }
    public List<int> Answers { get; set; } = [];
}
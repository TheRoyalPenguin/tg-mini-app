namespace Core.Models;

public class SubmitAnswersCommand
{
    public int ModuleId { get; set; }
    public List<int> Answers { get; set; } = [];
}
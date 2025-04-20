namespace Core.Models;

public class SubmitAnswersResult
{
    public bool IsSuccess { get; set; }
    public int AnswerCount { get; set; }
    public int CorrectCount { get; set; }
    public List<bool> Correctness { get; set; } = [];
}
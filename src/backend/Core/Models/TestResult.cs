namespace Core.Models;

public class TestResult
{
    public int Id { get; set; }
    public required int UserId { get; set; }
    public required int AttemptNumber { get; set; }
    public required int TotalQuestionsCount { get; set; }
    public required int CorrectAnswersCount { get; set; }
    public required int WrongAnswersCount { get; set; }
    public required float Score { get; set; }
    public required DateTime Timestamp { get; set; }
    
    public int TestId { get; set; }
    public Test Test { get; set; } 
}

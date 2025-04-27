namespace API.DTO;

public class AddStatisticRequest
{
    public required int UserId { get; set; }
    public required int AttemptNumber { get; set; }
    public required int TotalQuestionsCount { get; set; }
    public required int CorrectAnswersCount { get; set; }
    public required int WrongAnswersCount { get; set; }
    public required float Score { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.Now;
    
    public int TestId { get; set; }
    public Test Test { get; set; } 
}
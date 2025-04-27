using Core.Models;

namespace API.DTO;

public class TestResultDto(TestResult testResult)
{
    public int AttemptNumber { get; set; } = testResult.AttemptNumber;
    public int TotalQuestionsCount { get; set; } = testResult.TotalQuestionsCount;
    public int CorrectAnswersCount { get; set; } = testResult.CorrectAnswersCount;
    public int WrongAnswersCount { get; set; } = testResult.WrongAnswersCount;
    public float Score { get; set; } = testResult.Score;
    public DateTime Timestamp { get; set; } = testResult.Timestamp;
}
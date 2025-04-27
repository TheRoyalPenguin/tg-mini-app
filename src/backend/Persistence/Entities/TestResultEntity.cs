using EfCore.Conventions.Attributes;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Entities;

public class TestResultEntity
{
    public int Id { get; set; }
    public long TgId { get; set; }
    public int AttemptNumber { get; set; }
    public int TotalQuestions { get; set; }
    public int CorrectAnswers { get; set; }
    public int WrongAnswers { get; set; }
    public float Score { get; set; }
    public DateTime Timestamp { get; set; }
    
    [OnDelete(DeleteBehavior.SetNull)]
    public int TestId { get; set; }
    public TestEntity Test { get; set; }
}

using EfCore.Conventions.Attributes;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Entities;

public class TestResultEntity
{
    public int Id { get; set; }
    public int AttemptNumber { get; set; }
    public int TotalQuestionsCount { get; set; }
    public int CorrectAnswersCount { get; set; }
    public int WrongAnswersCount { get; set; }
    public float Score { get; set; }
    public DateTime Timestamp { get; set; }
    
    [OnDelete(DeleteBehavior.Cascade)]
    public int UserId { get; set; }
    public UserEntity User { get; set; }
    
    [OnDelete(DeleteBehavior.SetNull)]
    public int TestId { get; set; }
    public TestEntity Test { get; set; }
}

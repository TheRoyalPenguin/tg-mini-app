using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Entities;

namespace Persistence.Configurations;

public class TestResultConfiguration : IEntityTypeConfiguration<TestResultEntity>
{
    public void Configure(EntityTypeBuilder<TestResultEntity> builder)
    {
        builder.ToTable("test_results");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.AttemptNumber)
            .HasColumnName("attempt_number");
            
        builder.Property(x => x.TotalQuestionsCount)
            .HasColumnName("total_questions_count");
            
        builder.Property(x => x.CorrectAnswersCount)
            .HasColumnName("correct_answers_count");
            
        builder.Property(x => x.WrongAnswersCount)
            .HasColumnName("wrong_answers_count");
            
        builder.Property(x => x.Score)
            .HasColumnName("score");
            
        builder.Property(x => x.Timestamp)
            .HasColumnName("timestamp");
        
        builder.Property(x => x.UserId)
            .HasColumnName("user_id");
        
        builder.Property(x => x.TestId)
            .HasColumnName("test_id");
    }
} 
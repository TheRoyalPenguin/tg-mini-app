using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Entities;

namespace Persistence.Configurations;

public class TestResultConfiguration : IEntityTypeConfiguration<TestResultEntity>
{
    public void Configure(EntityTypeBuilder<TestResultEntity> builder)
    {
        builder.ToTable("test_results");
        
        builder.HasKey(tr => tr.Id)
            .HasName("test_results_pkey");

        builder.Property(tr => tr.Id)
            .HasColumnName("test_result_id");

        builder.Property(tr => tr.UserId)
            .HasColumnName("user_id");

        builder.Property(tr => tr.TestId)
            .HasColumnName("test_id");

        builder.Property(tr => tr.AttemptNumber)
            .HasColumnName("attempt_number");

        builder.Property(tr => tr.TotalQuestionsCount)
            .HasColumnName("total_questions_count");

        builder.Property(tr => tr.CorrectAnswersCount)
            .HasColumnName("correct_answers_count");

        builder.Property(tr => tr.WrongAnswersCount)
            .HasColumnName("wrong_answers_count");

        builder.Property(tr => tr.Score)
            .HasColumnName("score");

        builder.Property(tr => tr.Timestamp)
            .HasColumnName("timestamp");
    }
}
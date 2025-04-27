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

        builder.Property(tr => tr.TgId)
            .HasColumnName("tg_id");

        builder.Property(tr => tr.TestId)
            .HasColumnName("test_id");

        builder.Property(tr => tr.AttemptNumber)
            .HasColumnName("attempt_number");

        builder.Property(tr => tr.TotalQuestions)
            .HasColumnName("total_questions");

        builder.Property(tr => tr.CorrectAnswers)
            .HasColumnName("correct_answers");

        builder.Property(tr => tr.WrongAnswers)
            .HasColumnName("wrong_answers");

        builder.Property(tr => tr.Score)
            .HasColumnName("score");

        builder.Property(tr => tr.Timestamp)
            .HasColumnName("timestamp");

        builder.HasMany(tr => tr.QuestionResults)
            .WithOne(qr => qr.TestResult)
            .HasForeignKey(qr => qr.TestResultId)
            .HasConstraintName("question_results_test_result_id_fkey");
    }
}

public class QuestionResultConfiguration : IEntityTypeConfiguration<QuestionResultEntity>
{
    public void Configure(EntityTypeBuilder<QuestionResultEntity> builder)
    {
        builder.ToTable("question_results");
        
        builder.HasKey(qr => qr.Id)
            .HasName("question_results_pkey");

        builder.Property(qr => qr.Id)
            .HasColumnName("question_result_id");

        builder.Property(qr => qr.TestResultId)
            .HasColumnName("test_result_id");

        builder.Property(qr => qr.QuestionId)
            .HasColumnName("question_id");

        builder.Property(qr => qr.IsCorrect)
            .HasColumnName("is_correct");

        builder.Property(qr => qr.UserAnswer)
            .HasColumnName("user_answer");

        builder.Property(qr => qr.CorrectAnswer)
            .HasColumnName("correct_answer");

        builder.Property(qr => qr.TimeSpent)
            .HasColumnName("time_spent");
    }
} 
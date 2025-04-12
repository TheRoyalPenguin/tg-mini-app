using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Entities;

namespace Persistence.Configurations;

public class LessonProgressConfiguration : IEntityTypeConfiguration<LessonProgressEntity>
{
    public void Configure(EntityTypeBuilder<LessonProgressEntity> builder)
    {
        builder.ToTable("lessons_progress");
        
        builder.HasKey(lp => lp.Id)
            .HasName("lessons_progress_pkey");

        builder.Property(lp => lp.Id)
            .HasColumnName("lesson_progress_id");
        
        builder.Property(lp => lp.IsLessonCompleted)
            .HasColumnName("is_lesson_completed")
            .HasDefaultValue(false);

        builder.Property(lp => lp.LastActivityDate)
            .HasColumnName("lesson_last_activity_date");
        
        builder.Property(lp => lp.UserId)
            .HasColumnName("user_id");

        builder.Property(lp => lp.LessonId)
            .HasColumnName("lesson_id");
            
        builder.HasIndex(lp => new {lp.UserId, lp.LessonId})
            .IsUnique();
    }
}
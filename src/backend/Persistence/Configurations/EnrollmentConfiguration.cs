using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Entities;

namespace Persistence.Configurations;

public class EnrollmentConfiguration : IEntityTypeConfiguration<EnrollmentEntity>
{
    public void Configure(EntityTypeBuilder<EnrollmentEntity> builder)
    {
        builder.ToTable("enrollments");
        
        builder.HasKey(e => e.Id)
            .HasName("enrollments_pkey");
        
        builder.Property(e => e.Id)
            .HasColumnName("enrollment_id")
            .HasDefaultValueSql("uuid_generate_v4()");
        
        builder.Property(e => e.IsCourseCompleted)
            .HasColumnName("is_course_completed")
            .HasDefaultValue(false);
        
        builder.Property(e => e.EnrollmentDate)
            .HasColumnName("enrollment_date")
            .HasDefaultValueSql("CURRENT_DATE");

        builder.Property(e => e.CompletionDate)
            .HasColumnName("course_completion_date");
        
        builder.Property(e => e.UserId)
            .HasColumnName("user_id");

        builder.Property(e => e.CourseId)
            .HasColumnName("course_id");
        
        builder.HasIndex(e => new { e.UserId, e.CourseId })
            .IsUnique();
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Entities;

namespace Persistence.Configurations;

public class CourseConfiguration : IEntityTypeConfiguration<CourseEntity>
{
    public void Configure(EntityTypeBuilder<CourseEntity> builder)
    {
        builder.ToTable("courses");
        
        builder.HasKey(c => c.Id)
            .HasName("courses_pkey");

        builder.Property(c => c.Id)
            .HasColumnName("course_id");
        
        builder.Property(c => c.Title)
            .HasColumnName("course_title")
            .HasMaxLength(100);

        builder.Property(c => c.Description)
            .HasColumnName("course_description");
    }
}
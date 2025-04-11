using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Entities;

namespace Persistence.Configurations;

public class LessonConfiguration : IEntityTypeConfiguration<LessonEntity>
{
    public void Configure(EntityTypeBuilder<LessonEntity> builder)
    {
        builder.ToTable("lessons");
        
        builder.HasKey(l => l.Id)
            .HasName("lessons_pk");

        builder.Property(l => l.Id)
            .HasColumnName("lesson_id");

        builder.Property(l => l.Title)
            .HasColumnName("lesson_title")
            .HasMaxLength(100);

        builder.Property(l => l.Description)
            .HasColumnName("lesson_description");

        builder.Property(l => l.ModuleId)
            .HasColumnName("module_id");
    }
}
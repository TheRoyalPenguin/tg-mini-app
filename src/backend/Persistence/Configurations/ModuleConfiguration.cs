using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Entities;

namespace Persistence.Configurations;

public class ModuleConfiguration : IEntityTypeConfiguration<ModuleEntity>
{
    public void Configure(EntityTypeBuilder<ModuleEntity> builder)
    {
        builder.ToTable("modules");

        builder.HasKey(m => m.Id)
            .HasName("modules_pkey");

        builder.Property(m => m.Id)
            .HasColumnName("module_id");

        builder.Property(m => m.Title)
            .HasColumnName("module_title")
            .HasMaxLength(100);

        builder.Property(m => m.Description)
            .HasColumnName("module_description");

        builder.Property(m => m.LongreadCount)
            .HasColumnName("module_longread_count");

        builder.Property(m => m.CourseId)
            .HasColumnName("course_id");
    }
}
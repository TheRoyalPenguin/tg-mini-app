using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Entities;

namespace Persistence.Configurations;

public class ResourceConfiguration : IEntityTypeConfiguration<ResourceEntity>
{
    public void Configure(EntityTypeBuilder<ResourceEntity> builder)
    {
        builder.ToTable("resources");
        
        builder.HasKey(r => r.Id)
            .HasName("resource_pkey");
        
        builder.Property(r => r.Id)
            .HasColumnName("resource_id")
            .HasDefaultValueSql("uuid_generate_v4()");

        builder.Property(r => r.Title)
            .HasColumnName("resource_title")
            .HasMaxLength(100);

        builder.Property(r => r.LessonId)
            .HasColumnName("lesson_id");
    }
}
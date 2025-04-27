using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Entities;

namespace Persistence.Configurations;

public class TestConfiguration : IEntityTypeConfiguration<TestEntity>
{
    public void Configure(EntityTypeBuilder<TestEntity> builder)
    {
        builder.ToTable("tests");

        builder.HasKey(t => t.Id)
            .HasName("tests_pkey");

        builder.Property(t => t.Id)
            .HasColumnName("test_id");

        builder.Property(t => t.Title)
            .HasColumnName("test_title")
            .HasMaxLength(250);

        builder.Property(t => t.JsonKey)
            .HasColumnName("json_key");

        builder.Property(t => t.CreatedAt)
            .HasColumnName("created_at")
            .HasDefaultValueSql("NOW()");

        builder.Property(t => t.ModuleId)
            .HasColumnName("module_id");

        builder.HasOne(t => t.Module)
            .WithMany(m => m.Tests)
            .HasForeignKey(t => t.ModuleId)
            .HasConstraintName("fk_tests_modules")
            .OnDelete(DeleteBehavior.Cascade);
    }
}

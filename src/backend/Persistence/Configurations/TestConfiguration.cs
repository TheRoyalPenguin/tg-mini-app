using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Entities;

namespace Persistence.Configurations;

public class TestConfiguration : IEntityTypeConfiguration<TestEntity>
{
    public void Configure(EntityTypeBuilder<TestEntity> builder)
    {
        builder.ToTable("testiki");
        
        builder.HasKey(t => t.Id)
            .HasName("testiki_pkey");

        builder.Property(t => t.Id)
            .HasColumnName("testiki_id")
            .HasDefaultValueSql("uuid_generate_v4()");

        builder.Property(t => t.Content)
            .HasColumnName("testiki_content")
            .HasMaxLength(100);
        
        builder.HasIndex(t => t.Content)
            .IsUnique();
    }
}
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
            .HasColumnName("resource_id");

        builder.Property(r => r.Type)
            .HasColumnName("resource_type")
            .HasMaxLength(20);

        builder.Property(r => r.JsonUri)
            .HasColumnName("resource_json_uri");

        builder.Property(r => r.ModuleId)
            .HasColumnName("module_id");
    }
}
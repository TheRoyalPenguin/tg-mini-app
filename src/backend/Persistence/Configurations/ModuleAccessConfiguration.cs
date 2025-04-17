using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Entities;

namespace Persistence.Configurations;

public class ModuleAccessConfiguration : IEntityTypeConfiguration<ModuleAccessEntity>
{
    public void Configure(EntityTypeBuilder<ModuleAccessEntity> builder)
    {
        builder.ToTable("module_accesses");
        
        builder.HasKey(ma => ma.Id)
            .HasName("module_accesses_pkey");

        builder.Property(ma => ma.Id)
            .HasColumnName("module_access_id");
        
        builder.Property(ma => ma.IsModuleCompleted)
            .HasColumnName("is_module_completed")
            .HasDefaultValue(false);

        builder.Property(ma => ma.CompletionDate)
            .HasColumnName("module_completion_date");
        
        builder.HasIndex(ma => new { ma.Id, ma.ModuleId })
            .IsUnique();
    }
}
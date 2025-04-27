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

        builder.Property(ma => ma.TestTriesCount)
            .HasColumnName("test_tries_count")
            .HasDefaultValue(0);
        
        builder.Property(ma => ma.IsModuleCompleted)
            .HasColumnName("is_module_completed")
            .HasDefaultValue(false);
        
        builder.Property(ma => ma.IsModuleAvailable)
            .HasColumnName("is_module_available")
            .HasDefaultValue(false);

        builder.Property(ma => ma.CompletionDate)
            .HasColumnName("module_completion_date");
        
        builder.Property(ma => ma.LastActivity)
            .HasColumnName("last_activity");
        
        builder.Property(e => e.UserId)
            .HasColumnName("user_id");

        builder.Property(e => e.ModuleId)
            .HasColumnName("module_id");
        
        builder.HasIndex(ma => new { ma.Id, ma.ModuleId })
            .IsUnique();
    }
}
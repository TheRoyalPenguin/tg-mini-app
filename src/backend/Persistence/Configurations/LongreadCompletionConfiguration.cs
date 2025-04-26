using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Entities;

namespace Persistence.Configurations;

public class LongreadCompletionConfiguration : IEntityTypeConfiguration<LongreadCompletionEntity>
{
    public void Configure(EntityTypeBuilder<LongreadCompletionEntity> builder)
    {
        builder.ToTable("longread_completions");

        builder.HasKey(lc => lc.Id)
            .HasName("longread_completions_pkey");

        builder.Property(lc => lc.Id)
            .HasColumnName("longread_completion_id");
        
        builder.Property(lc => lc.ModuleAccessId)
            .HasColumnName("user_id");

        builder.Property(lc => lc.ResourceId)
            .HasColumnName("resource_id");
        
        builder.HasIndex(ma => new { ma.ResourceId, ma.ModuleAccessId })
            .IsUnique();
    }
}
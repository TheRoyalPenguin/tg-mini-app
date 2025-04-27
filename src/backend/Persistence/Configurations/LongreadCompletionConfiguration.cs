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
            .HasColumnName("module_access_id");

        builder.Property(lc => lc.LongreadId)
            .HasColumnName("longread_id");
        
        builder.HasIndex(ma => new { ma.LongreadId, ma.ModuleAccessId})
            .IsUnique();
    }
}
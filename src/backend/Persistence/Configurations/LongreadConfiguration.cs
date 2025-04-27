
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Persistence.Entities;

namespace Persistence.Configurations;

public class LongreadConfiguration : IEntityTypeConfiguration<LongreadEntity>
{
    public void Configure(EntityTypeBuilder<LongreadEntity> builder)
    {
        builder.ToTable("longreads");

        builder.HasKey(l => l.Id)
            .HasName("longreads_pkey");

        builder.Property(l => l.Id)
            .HasColumnName("longread_id");

        builder.Property(l => l.Title)
            .HasColumnName("longread_title")
            .HasMaxLength(250);

        builder.Property(l => l.Description)
            .HasColumnName("longread_description");

        builder.Property(l => l.HtmlContentKey)
            .HasColumnName("html_content_key");

        builder.Property(l => l.OriginalDocxKey)
            .HasColumnName("original_docx_key");

        builder.Property(l => l.ModuleId)
            .HasColumnName("module_id");

        builder.HasOne(l => l.Module)
            .WithMany(m => m.Longreads)
            .HasForeignKey(l => l.ModuleId)
            .HasConstraintName("fk_longreads_modules")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(l => l.Images)
            .WithOne(i => i.Longread)
            .HasForeignKey(i => i.LongreadEntityId)
            .HasConstraintName("fk_images_longreads")
            .OnDelete(DeleteBehavior.Cascade);
    }
}

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Persistence.Entities;

namespace Persistence.Configurations;

public class LongreadImageConfiguration : IEntityTypeConfiguration<LongreadImageEntity>
{
    public void Configure(EntityTypeBuilder<LongreadImageEntity> builder)
    {
        builder.ToTable("longread_images");

        builder.HasKey(i => i.Id)
            .HasName("longread_images_pkey");

        builder.Property(i => i.Id)
            .HasColumnName("image_id");

        builder.Property(i => i.Key)
            .HasColumnName("image_key");

        builder.Property(i => i.LongreadEntityId)
            .HasColumnName("longread_id");

        builder.HasOne(i => i.Longread)
            .WithMany(l => l.Images)
            .HasForeignKey(i => i.LongreadEntityId)
            .HasConstraintName("fk_images_longreads")
            .OnDelete(DeleteBehavior.Cascade);
    }
}

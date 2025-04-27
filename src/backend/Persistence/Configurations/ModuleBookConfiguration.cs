using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Persistence.Entities;

namespace Persistence.Configurations;

public class ModuleBookConfiguration : IEntityTypeConfiguration<ModuleBookEntity>
{
    public void Configure(EntityTypeBuilder<ModuleBookEntity> builder)
    {
        builder.ToTable("module_books");

        builder.HasKey(mb => new { mb.ModuleId, mb.BookId })
            .HasName("module_books_pkey");

        builder.Property(mb => mb.ModuleId)
            .HasColumnName("module_id");

        builder.Property(mb => mb.BookId)
            .HasColumnName("book_id");

        builder.HasOne(mb => mb.Module)
            .WithMany(m => m.ModuleBooks)
            .HasForeignKey(mb => mb.ModuleId)
            .HasConstraintName("fk_module_books_modules")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(mb => mb.Book)
            .WithMany(b => b.ModuleBooks)
            .HasForeignKey(mb => mb.BookId)
            .HasConstraintName("fk_module_books_books")
            .OnDelete(DeleteBehavior.Cascade);
    }
}

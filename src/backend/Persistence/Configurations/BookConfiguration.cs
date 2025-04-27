using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Persistence.Entities;

namespace Persistence.Configurations;

public class BookConfiguration : IEntityTypeConfiguration<BookEntity>
{
    public void Configure(EntityTypeBuilder<BookEntity> builder)
    {
        builder.ToTable("books");

        builder.HasKey(b => b.Id)
            .HasName("books_pkey");

        builder.Property(b => b.Id)
            .HasColumnName("book_id");

        builder.Property(b => b.Title)
            .HasColumnName("book_title")
            .HasMaxLength(300);

        builder.Property(b => b.Author)
            .HasColumnName("book_author")
            .HasMaxLength(200);

        builder.Property(b => b.FileKey)
            .HasColumnName("file_key");

        builder.Property(b => b.CoverKey)
            .HasColumnName("cover_key");
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Entities;

namespace Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.ToTable("users");
        
        builder.HasKey(u => u.Id)
            .HasName("users_pkey");

        builder.Property(u => u.Id)
            .HasColumnName("user_id");

        builder.Property(u => u.TgId)
            .HasColumnName("user_telegram_id");
        
        builder.Property(u => u.Name)
            .HasColumnName("user_first_name")
            .HasMaxLength(100);
        
        builder.Property(u => u.Surname)
            .HasColumnName("user_last_name")
            .HasMaxLength(100);
        
        builder.Property(u => u.Patronymic)
            .HasColumnName("user_patronymic")
            .HasMaxLength(100);
        
        builder.Property(u => u.PhoneNumber)
            .HasColumnName("user_phone_number")
            .HasMaxLength(20);
        
        builder.Property(u => u.IsBanned)
            .HasColumnName("is_banned")
            .HasDefaultValue(false);
        
        builder.Property(u => u.RoleId)
            .HasColumnName("role_id")
            .HasDefaultValue(0);

        builder.HasIndex(u => u.TgId)
            .IsUnique();
        
        builder.HasIndex(u => u.PhoneNumber)
            .IsUnique();
    }
}
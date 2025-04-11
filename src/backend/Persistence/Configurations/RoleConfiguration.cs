using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Entities;

namespace Persistence.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<RoleEntity>
{
    public void Configure(EntityTypeBuilder<RoleEntity> builder)
    {
        builder.ToTable("roles");
        
        builder.HasKey(r => r.Id)
            .HasName("role_pkey");

        builder.Property(r => r.Id)
            .ValueGeneratedOnAdd();

        builder.Property(r => r.Name)
            .HasColumnName("role_name")
            .HasMaxLength(50);

        builder.Property(r => r.RoleLevel)
            .HasColumnName("role_level");
        
        builder.HasIndex(r => r.Name)
            .IsUnique();
        
        builder.HasIndex(r => r.RoleLevel)
            .IsUnique();
    }
}
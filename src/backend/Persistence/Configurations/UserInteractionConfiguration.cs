using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Entities;

namespace Persistence.Configurations;

public class UserInteractionConfiguration : IEntityTypeConfiguration<UserInteractionEntity>
{
    public void Configure(EntityTypeBuilder<UserInteractionEntity> builder)
    {
        builder.ToTable("user_interactions");
        
        builder.HasKey(ui => ui.Id)
            .HasName("user_interactions_pkey");

        builder.Property(ui => ui.Id)
            .HasColumnName("user_interaction_id");

        builder.Property(ui => ui.TgId)
            .HasColumnName("tg_id");

        builder.Property(ui => ui.ActionType)
            .HasColumnName("action_type");

        builder.Property(ui => ui.ActionName)
            .HasColumnName("action_name");

        builder.Property(ui => ui.AdditionalData)
            .HasColumnName("additional_data");

        builder.Property(ui => ui.Timestamp)
            .HasColumnName("timestamp");
    }
} 
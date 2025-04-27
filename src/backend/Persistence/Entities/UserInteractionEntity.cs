using EfCore.Conventions.Attributes;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Entities;

public class UserInteractionEntity
{
    public int Id { get; set; }
    
    public required long TgId { get; set; }
    public required string ActionType { get; set; }
    public required string ActionName { get; set; }
    public string? AdditionalData { get; set; }
    public required DateTime Timestamp { get; set; }
} 
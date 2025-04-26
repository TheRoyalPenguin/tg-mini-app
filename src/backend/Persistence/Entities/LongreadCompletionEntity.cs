using Core.Models;
using EfCore.Conventions.Attributes;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Entities;

public class LongreadCompletionEntity
{
    public int Id { get; set; }
    
    [OnDelete(DeleteBehavior.Cascade)]
    public int ModuleAccessId { get; set; }
    public ModuleAccessEntity ModuleAccess { get; set; }

    [OnDelete(DeleteBehavior.Cascade)]
    public int ResourceId { get; set; }
    public ResourceEntity Resource { get; set; }
}
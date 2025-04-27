using Core.Models;

namespace API.DTO.User;

public class UserModuleStatistic(ModuleAccess moduleAccess)
{
    public int ModuleId { get; set; } = moduleAccess.ModuleId;
    public int ModuleAccessId { get; set; } = moduleAccess.Id;

    public bool IsModuleAvailable { get; set; } = moduleAccess.IsModuleAvailable;

    public float ModuleCompletionPercentage { get; set; } = moduleAccess.LongreadCompletions.Count == 0
        ? 0
        : (float)moduleAccess.LongreadCompletions.Count / (moduleAccess.ModuleLongreadCount + 1);
}
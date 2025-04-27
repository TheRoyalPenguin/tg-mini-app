using Core.Models;

namespace API.DTO.User;

public class UserFullModuleStatistic
{
    public int ModuleId { get; set; }
    public int ModuleAccessId { get; set; }

    public bool IsModuleAvailable { get; set; }

    public List<int> CompletedLongreadsIds { get; set; } = [];
    public int TestTriesCount { get; set; }

    public float ModuleCompletionPercentage { get; set; }
    public bool IsModuleCompleted { get; set; }
    public DateOnly? CompletionDate { get; set; }
    public DateTime? LastActivity { get; set; }

    public List<TestResult> TestResults { get; set; } = [];

    public UserFullModuleStatistic(ModuleAccess moduleAccess)
    {
        ModuleId = moduleAccess.ModuleId;
        ModuleAccessId = moduleAccess.Id;
        IsModuleAvailable = moduleAccess.IsModuleAvailable;
        IsModuleCompleted = moduleAccess.IsModuleCompleted;
        CompletedLongreadsIds = moduleAccess.LongreadCompletions.Select(x => x.LongreadId).ToList();
        TestTriesCount = moduleAccess.TestTriesCount;
        ModuleCompletionPercentage = CompletedLongreadsIds.Count == 0
            ? 0
            : (float)CompletedLongreadsIds.Count / (moduleAccess.ModuleLongreadCount + 1);
        CompletionDate = moduleAccess.CompletionDate;
        LastActivity = moduleAccess.LastActivity;
    }
}
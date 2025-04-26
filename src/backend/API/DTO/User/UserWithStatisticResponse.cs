using Core.Models;

namespace API.DTO.User;

public class UserWithStatisticResponse
{
    public int Id { get; set; }
    public long TgId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Patronymic { get; set; }
    public string PhoneNumber { get; set; }
    public bool IsBanned { get; set; }

    public Dictionary<int, List<ModuleStatistic>> CoursesStatistic { get; set; } = [];

    public UserWithStatisticResponse(Core.Models.User user)
    {
        Id = user.Id;
        TgId = user.TgId;
        Name = user.Name;
        Surname = user.Surname;
        Patronymic = user.Patronymic;
        PhoneNumber = user.PhoneNumber;
        IsBanned = user.IsBanned;
        
        foreach (var moduleAccess in user.ModuleAccesses)
            if (!CoursesStatistic.ContainsKey(moduleAccess.Module!.CourseId))
                CoursesStatistic[moduleAccess.Module!.CourseId] = [new ModuleStatistic(moduleAccess)];
            else
                CoursesStatistic[moduleAccess.Module!.CourseId].Add(new ModuleStatistic(moduleAccess));
    }
    
    public class ModuleStatistic
    {
        public int ModuleId { get; set; }
        public int ModuleAccessId { get; set; }

        public bool IsModuleAvailable { get; set; }

        public List<int> CompletedLongreadsIds { get; set; } = [];
        public int TestTriesCount { get; set; }

        public float ModuleCompletionPercentage { get; set; }
        public bool IsModuleCompleted { get; set; }
        public DateOnly? CompletionDate { get; set; }

        public ModuleStatistic(ModuleAccess moduleAccess)
        {
            ModuleId = moduleAccess.ModuleId;
            ModuleAccessId = moduleAccess.Id;
            IsModuleAvailable = moduleAccess.IsModuleAvailable;
            IsModuleCompleted = moduleAccess.IsModuleCompleted;
            CompletedLongreadsIds = moduleAccess.LongreadCompletions.Select(x => x.ResourceId).ToList();
            TestTriesCount = moduleAccess.TestTriesCount;
            ModuleCompletionPercentage = CompletedLongreadsIds.Count == 0
                ? 0
                : (float)(moduleAccess.ModuleLongreadCount + 1) / CompletedLongreadsIds.Count;
            CompletionDate = moduleAccess.CompletionDate;
        }
    }
    

    
}
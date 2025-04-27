using Core.Models;

namespace API.DTO.User;

public class UserWithStatisticResponse
{
    public int Id { get; set; }
    public long TgId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }

    public Dictionary<int, List<UserModuleStatistic>> CoursesStatistic { get; set; } = [];

    public UserWithStatisticResponse(Core.Models.User user)
    {
        Id = user.Id;
        TgId = user.TgId;
        Name = user.Name;
        Surname = user.Surname;
        
        foreach (var moduleAccess in user.ModuleAccesses)
            if (!CoursesStatistic.ContainsKey(moduleAccess.Module!.CourseId))
                CoursesStatistic[moduleAccess.Module!.CourseId] = [new UserModuleStatistic(moduleAccess)];
            else
                CoursesStatistic[moduleAccess.Module!.CourseId].Add(new UserModuleStatistic(moduleAccess));
    }
}
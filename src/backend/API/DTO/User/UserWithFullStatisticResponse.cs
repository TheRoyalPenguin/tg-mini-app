using Core.Models;

namespace API.DTO.User;

public class UserWithFullStatisticResponse
{
    public int Id { get; set; }
    public long TgId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Patronymic { get; set; }
    public string PhoneNumber { get; set; }
    public bool IsBanned { get; set; }

    public Dictionary<int, List<UserFullModuleStatistic>> CoursesStatistic { get; set; } = [];

    public UserWithFullStatisticResponse(Core.Models.User user)
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
                CoursesStatistic[moduleAccess.Module!.CourseId] = [new UserFullModuleStatistic(moduleAccess)];
            else
                CoursesStatistic[moduleAccess.Module!.CourseId].Add(new UserFullModuleStatistic(moduleAccess));
    }
}
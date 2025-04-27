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

    public UserWithFullStatisticResponse(Core.Models.User user, ICollection<TestResult> testResults)
    {
        Id = user.Id;
        TgId = user.TgId;
        Name = user.Name;
        Surname = user.Surname;
        Patronymic = user.Patronymic;
        PhoneNumber = user.PhoneNumber;
        IsBanned = user.IsBanned;
        var testResultsDict = testResults.GroupBy(tr => tr.Test.ModuleId).ToDictionary(group => group.Key, group => group.ToList());
        
        foreach (var moduleAccess in user.ModuleAccesses)
            if (!CoursesStatistic.ContainsKey(moduleAccess.Module!.CourseId))
                CoursesStatistic[moduleAccess.Module!.CourseId] = [new UserFullModuleStatistic(moduleAccess, testResultsDict[moduleAccess.Module!.CourseId])];
            else
                CoursesStatistic[moduleAccess.Module!.CourseId].Add(new UserFullModuleStatistic(moduleAccess, testResultsDict[moduleAccess.Module!.CourseId]));
    }
}
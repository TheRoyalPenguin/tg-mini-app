using API.DTO.ModuleAccess;
using API.DTO.ModuleAccessRequests;

namespace API.DTO.User;

public class UserInfoByCourseWithModuleAccesses
{
    public int Id { get; set; }
    public required long TgId { get; set; }
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public required string Patronymic { get; set; }
    public required string PhoneNumber { get; set; }
    public bool IsBanned { get; set; } = false;
    public required int RoleId { get; set; }
    public required DateTime RegisteredAt { get; set; }
    public List<ModuleAccessWithProgress> ModuleAccesses { get; set; } = new();
}
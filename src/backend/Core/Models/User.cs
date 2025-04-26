namespace Core.Models;

public class User
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
    public List<ModuleAccess> ModuleAccesses { get; set; } = new();
}
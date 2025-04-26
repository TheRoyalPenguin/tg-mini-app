namespace Persistence.Entities;

public class RoleEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int RoleLevel { get; set; }
}
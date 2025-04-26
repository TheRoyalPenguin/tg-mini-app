namespace Persistence.Entities;

public class RoleEntity
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int RoleLevel { get; set; }
}
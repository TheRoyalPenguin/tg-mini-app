namespace Persistence.Entities;

public class RoleEntity
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required int RoleLevel { get; set; }
}
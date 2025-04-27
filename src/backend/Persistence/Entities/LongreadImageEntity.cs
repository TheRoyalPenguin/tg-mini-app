namespace Persistence.Entities;

public class LongreadImageEntity
{
    public int Id { get; set; }
    public string Key { get; set; } = null!;

    public int LongreadEntityId { get; set; }
    public LongreadEntity Longread { get; set; } = null!;
}

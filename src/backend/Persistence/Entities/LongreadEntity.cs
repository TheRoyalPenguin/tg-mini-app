namespace Persistence.Entities;

public class LongreadEntity
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string HtmlContentKey { get; set; } = null!;
    public string OriginalDocxKey { get; set; } = null!;

    public int ModuleId { get; set; }
    public ModuleEntity Module { get; set; } = null!;

    public ICollection<LongreadImageEntity> Images { get; set; } = new List<LongreadImageEntity>();
}

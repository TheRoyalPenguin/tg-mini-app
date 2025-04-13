namespace Core.Models;

public class TgUserModel
{
    public required long TgId { get; set; }
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public required string Patronymic { get; set; }
    public required string PhoneNumber { get; set; }
}

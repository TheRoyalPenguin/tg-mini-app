namespace API.DTO;

public class TelegramMiniAppAuthRequest
{
    public required long TgId { get; set; }
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public required string Patronymic { get; set; }
    public required string PhoneNumber { get; set; }
    public required string Hash { get; set; }
}

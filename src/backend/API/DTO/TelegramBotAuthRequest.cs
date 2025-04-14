namespace API.DTO;

public class TelegramBotAuthRequest
{
    public required long TgId { get; set; }
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public required string PhoneNumber { get; set; }
}

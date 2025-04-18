namespace API.DTO;

public class InitDataRequest
{
    public string InitData { get; set; } = null!;
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public required string Patronymic { get; set; }
    public required string PhoneNumber { get; set; }
}

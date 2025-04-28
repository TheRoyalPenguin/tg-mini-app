namespace API.DTO.User;

public class UserDto
{
    public int Id { get; set; }
    public long TgId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    
    public UserDto(Core.Models.User user)
    {
        Id = user.Id;
        TgId = user.TgId;
        Name = user.Name;
        Surname = user.Surname;
    }
}
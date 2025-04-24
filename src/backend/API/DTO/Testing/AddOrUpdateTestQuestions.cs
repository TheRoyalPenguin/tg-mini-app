namespace API.DTO.Testing;

public class AddOrUpdateTestQuestions
{
    public required string Question { get; set; }
    public List<string> Options { get; set; } = [];
    public int CorrectAnswer { get; set; }
}
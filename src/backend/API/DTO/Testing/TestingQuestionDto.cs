namespace API.DTO.Testing;

public record TestingQuestionDto(string Question, List<string> Options, int CorrectAnswer);
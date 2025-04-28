namespace API.DTO.Testing;

public record SubmitAnswersResultDto(
    bool IsSuccess,
    int AnswerCount,
    int CorrectCount,
    List<bool> Correctness
);
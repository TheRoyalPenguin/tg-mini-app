using API.DTO;
using FluentValidation;

namespace API.Validators;

public class CreateLongreadDtoValidator : AbstractValidator<CreateLongreadDto>
{
    // 10 МБ
    private const long MaxDocxSize = 10 * 1024 * 1024;
    // 20 МБ
    private const long MaxAudioSize = 20 * 1024 * 1024;

    public CreateLongreadDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Заголовок обязателен.")
            .MaximumLength(200).WithMessage("Заголовок не может быть длиннее 200 символов.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Описание обязательно.")
            .MaximumLength(1000).WithMessage("Описание не может быть длиннее 1000 символов.");

        RuleFor(x => x.DocxFile)
            .NotNull().WithMessage("Файл DOCX обязателен.")
            .Must(HaveDocxExtension).WithMessage("Файл должен иметь расширение .docx.")
            .Must(f => f.Length > 0).WithMessage("Файл DOCX не может быть пустым.")
            .Must(f => f.Length <= MaxDocxSize).WithMessage($"Размер DOCX не должен превышать {MaxDocxSize / (1024 * 1024)} МБ.")
            .Must(HaveDocxContentType).WithMessage("Неверный MIME-тип для DOCX.");

        RuleFor(x => x.AudioFile)
            .Cascade(CascadeMode.Stop)
            .Must(f => f == null || f.Length > 0)
                .WithMessage("Аудиофайл не может быть пустым.")
            .Must(f => f == null || f.Length <= MaxAudioSize)
                .WithMessage($"Размер аудиофайла не должен превышать {MaxAudioSize / (1024 * 1024)} МБ.")
            .Must(f => f == null || HaveAudioExtension(f))
                .WithMessage("Аудиофайл должен иметь расширение .mp3, .wav или .m4a")
            .Must(f => f == null || HaveAudioContentType(f))
                .WithMessage("Неверный MIME-тип для аудиофайла.");
    }

    private bool HaveDocxExtension(IFormFile f) =>
        Path.GetExtension(f.FileName).Equals(".docx", StringComparison.OrdinalIgnoreCase);

    private bool HaveDocxContentType(IFormFile f) =>
        f.ContentType == "application/vnd.openxmlformats-officedocument.wordprocessingml.document";

    private bool HaveAudioExtension(IFormFile f)
    {
        var ext = Path.GetExtension(f.FileName).ToLowerInvariant();
        return ext == ".mp3" || ext == ".wav" || ext ==".m4a";
    }

    private bool HaveAudioContentType(IFormFile f)
    {
        return f.ContentType.StartsWith("audio/");
    }
}

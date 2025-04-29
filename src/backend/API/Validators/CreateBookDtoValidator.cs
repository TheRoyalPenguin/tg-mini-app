using API.DTO.Book;
using FluentValidation;

namespace API.Validators;

public class CreateBookDtoValidator : AbstractValidator<CreateBookDto>
{
    // 20 МБ для книги
    private const long MaxContentSize = 20 * 1024 * 1024;
    // 5 МБ для обложки
    private const long MaxCoverSize = 5 * 1024 * 1024;

    public CreateBookDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Название книги обязательно.")
            .MaximumLength(200).WithMessage("Название не может быть длиннее 200 символов.");

        RuleFor(x => x.Author)
            .NotEmpty().WithMessage("Автор обязателен.")
            .MaximumLength(100).WithMessage("Имя автора не может быть длиннее 100 символов.");

        RuleFor(x => x.ContentFile)
            .NotNull().WithMessage("Файл книги обязателен.")
            .Must(HaveValidBookExtension).WithMessage("Допустимые форматы: .pdf, .epub, .fb2")
            .Must(f => f.Length > 0).WithMessage("Файл книги не может быть пустым.")
            .Must(f => f.Length <= MaxContentSize).WithMessage($"Размер файла не должен превышать {MaxContentSize / (1024 * 1024)} МБ.")
            .Must(HaveValidBookContentType).WithMessage("Неверный MIME-тип для книги.");

        RuleFor(x => x.CoverFile)
            .Cascade(CascadeMode.Stop)
            .Must(f => f == null || f.Length > 0)
                .WithMessage("Файл обложки не может быть пустым.")
            .Must(f => f == null || f.Length <= MaxCoverSize)
                .WithMessage($"Размер обложки не должен превышать {MaxCoverSize / (1024 * 1024)} МБ.")
            .Must(f => f == null || HaveValidImageExtension(f))
                .WithMessage("Допустимые форматы: .jpg, .jpeg, .png, .webp")
            .Must(f => f == null || HaveValidImageContentType(f))
                .WithMessage("Неверный MIME-тип для изображения.");
    }
    private bool HaveValidBookExtension(IFormFile file)
    {
        var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
        return ext switch
        {
            ".pdf" => true,
            ".epub" => true,
            ".fb2" => true,
            _ => false
        };
    }
    private bool HaveValidBookContentType(IFormFile file)
    {
        return file.ContentType switch
        {
            "application/pdf" => true,
            "application/epub+zip" => true,
            "application/x-fictionbook+xml" => true,
            "text/xml" => true, // Для FB2
            _ => false
        };
    }
    private bool HaveValidImageExtension(IFormFile file)
    {
        var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
        return ext switch
        {
            ".jpg" => true,
            ".jpeg" => true,
            ".png" => true,
            ".webp" => true,
            _ => false
        };
    }
    private bool HaveValidImageContentType(IFormFile file)
    {
        return file.ContentType switch
        {
            "image/jpeg" => true,
            "image/png" => true,
            "image/webp" => true,
            _ => false
        };
    }
}

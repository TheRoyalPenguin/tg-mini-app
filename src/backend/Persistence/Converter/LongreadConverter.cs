using Core.Interfaces.Services;
using Core.Models;
using System.Text;

namespace Persistence.Converter;

public class LongreadConverter : ILongreadConverter
{
    private readonly IStorageService _storage;
    private readonly DocxConverter _mammoth;

    public LongreadConverter(
        IStorageService storage,
        DocxConverter mammothConverter)
    {
        _storage = storage;
        _mammoth = mammothConverter;
    }

    public async Task<ConvertedLongread> ConvertAsync(
        Stream docxStream,
        string docxFileName,
        int moduleId,
        CancellationToken ct = default)
    {
        var folder = $"modules/{moduleId}/longreads/{Guid.NewGuid()}";

        var docxKey = $"{folder}/{docxFileName}";
        if (docxStream.CanSeek) docxStream.Position = 0;
        await _storage.UploadAsync(docxStream, docxKey, ct);

        if (docxStream.CanSeek) docxStream.Position = 0;
        var html = _mammoth.ConvertToHtml(docxStream);

        var htmlKey = $"{folder}/{Path.GetFileNameWithoutExtension(docxFileName)}.html";
        var htmlBytes = Encoding.UTF8.GetBytes(html);
        await using var htmlStream = new MemoryStream(htmlBytes, writable: false);
        await _storage.UploadAsync(htmlStream, htmlKey, ct);

        // Пока без картинок
        return new ConvertedLongread
        {
            OriginalDocxKey = docxKey,
            HtmlKey = htmlKey,
            ImageKeys = Array.Empty<string>()
        };
    }
}

using Core.Interfaces.Services;
using Core.Models;
using Microsoft.Extensions.Logging;
using System.Text;

namespace Persistence.Converter;

public class LongreadConverter : ILongreadConverter
{
    private readonly IStorageService _storage;
    private readonly DocxConverter _mammoth;
    private readonly ILogger<LongreadConverter> _logger;

    public LongreadConverter(
        IStorageService storage,
        DocxConverter mammothConverter,
        ILogger<LongreadConverter> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _storage = storage;
        _mammoth = mammothConverter;
    }

    public async Task<ConvertedLongread> ConvertAsync(
        Stream docxStream,
        string docxFileName,
        Stream? audioStream,
        string? audioFileName,
        int moduleId,
        CancellationToken ct = default)
    {
        var folder = $"modules/{moduleId}/longreads/{Guid.NewGuid()}";

        var docxKey = $"{folder}/{docxFileName}";
        string? audioKey = default;

        if (audioFileName != null && audioStream != null)
        {
            audioKey = $"{folder}/{audioFileName}";

            if (audioStream.CanSeek) audioStream.Position = 0;
            await _storage.UploadAsync(audioStream, audioKey, ct);
        }
        else
        {
            _logger.LogWarning("AudioStream или AudioFileName равны null, их загрузка не выполнена.");
        }

        if (docxStream.CanSeek) docxStream.Position = 0;
        await _storage.UploadAsync(docxStream, docxKey, ct);

        if (docxStream.CanSeek) docxStream.Position = 0;
        var html = _mammoth.ConvertToHtml(docxStream);

        var htmlKey = $"{folder}/{Path.GetFileNameWithoutExtension(docxFileName)}.html";
        var htmlBytes = Encoding.UTF8.GetBytes(html);
        await using var htmlStream = new MemoryStream(htmlBytes, writable: false);
        await _storage.UploadAsync(htmlStream, htmlKey, ct);

        return new ConvertedLongread
        {
            OriginalDocxKey = docxKey,
            HtmlKey = htmlKey,
            AudioKey = string.IsNullOrEmpty(audioKey) ? null : audioKey,
            ImageKeys = Array.Empty<string>() // картинки в самом html
        };
    }
}

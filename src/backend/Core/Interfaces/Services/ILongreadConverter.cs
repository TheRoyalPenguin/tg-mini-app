using Core.Models;
using System;
namespace Core.Interfaces.Services;

public interface ILongreadConverter
{
    Task<ConvertedLongread> ConvertAsync(
        Stream docxStream,
        string docxFileName,
        int moduleId,
        CancellationToken ct = default);
}

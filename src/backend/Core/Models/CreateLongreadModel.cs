using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models;

public class CreateLongreadModel
{
    public int ModuleId { get; init; }
    public string Title { get; init; } = null!;
    public string Description { get; init; } = null!;

    public Stream DocxStream { get; init; } = null!;
    public string DocxFileName { get; init; } = null!;

    public Stream? AudioStream { get; init; }
    public string? AudioFileName { get; init; }
}

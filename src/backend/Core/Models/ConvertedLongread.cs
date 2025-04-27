using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models;

public class ConvertedLongread
{
    public string OriginalDocxKey { get; init; } = null!;
    public string HtmlKey { get; init; } = null!;
    public IReadOnlyList<string> ImageKeys { get; init; } = Array.Empty<string>();
}

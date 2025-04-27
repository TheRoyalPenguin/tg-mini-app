using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models;

public class Longread
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string HtmlContentKey { get; set; } = null!;
    public List<string> ImageKeys { get; set; } = new();
    public string OriginalDocxKey { get; set; } = null!;
}

using Mammoth;

namespace Persistence.Converter;

public class DocxConverter
{
    public string ConvertToHtml(Stream docxStream)
    {
        var converter = new DocumentConverter();
        var result = converter.ConvertToHtml(docxStream);

        return AddTableStyles(result.Value);
    }

    private string AddTableStyles(string html)
    {
        const string tableStyles = @"
            <style>
              table {
                border-collapse: collapse;
                width: 100%;
              }
              table, th, td {
                border: 1px solid black;
              }
              th, td {
                padding: 8px;
                text-align: left;
              }
            </style>
            ";
        if (html.Contains("</head>"))
            return html.Replace("</head>", tableStyles + "</head>");
        if (html.Contains("<body>"))
            return html.Replace("<body>", "<body>" + tableStyles);
        return tableStyles + html;
    }
}

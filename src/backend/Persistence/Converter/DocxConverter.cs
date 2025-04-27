using Mammoth;

namespace Persistence.Converter;

public class DocxConverter
{
    public string ConvertToHtml(Stream docxStream)
    {
        var converter = new DocumentConverter();
        var result = converter.ConvertToHtml(docxStream);

        var htmlWithTableWrapper = result.Value.Replace("<table", "<div class=\"table-container\"><table").Replace("</table>", "</table></div>");

        var htmlWithStyles = AddTableStyles(htmlWithTableWrapper);
        return htmlWithStyles;
    }

    private string AddTableStyles(string html)
    {
        const string tableStyles = @"
        <meta charset=""utf-8"">
        <meta name=""viewport"" content=""width=device-width, initial-scale=1"">
        <style>
            body {
                margin: 0;
                padding: 1rem;
                font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
                background: #f5f5f5;
                color: #333;
                line-height: 1.6;
                box-sizing: border-box;
            }
            h1, h2, h3, h4 {
                margin: 1.5rem 0 0.5rem;
                font-weight: 700;
            }
            p {
                margin-bottom: 1rem;
            }
            img {
                max-width: 100%;
                height: auto;
                display: block;
            }
            .table-container {
                overflow-x: auto;
                width: 100%;
                background: #fff;
                border-radius: 0.5rem;
                box-shadow: 0 4px 12px rgba(0,0,0,0.1);
                margin-bottom: 1.5rem;
            }
            table {
                border-collapse: collapse;
                width: 100%;
                min-width: 600px;
            }
            table, th, td {
                border: 1px solid #ccc;
            }
            th, td {
                padding: 0.75rem 1rem;
                text-align: left;
                border: 1px solid #e0e0e0;
            }
            th {
                background: #fafafa;
                font-weight: 700;
            }
            tr:nth-child(even) {
                background: #fcfcfc;
            }
            tr:hover {
                background: #f1f1f1;
            }
            br {
                display: block;
                margin: 0.5em 0;
            }
        </style>
        ";

        if (html.Contains("</head>"))
        {
            return html.Replace("</head>", tableStyles + "</head>");
        }
        if (html.Contains("<body>"))
        {
            return html.Replace("<body>", "<body>" + tableStyles);
        }

        return tableStyles + html;
    }
}

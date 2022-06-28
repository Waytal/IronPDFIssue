using IronPdf;
using Microsoft.AspNetCore.Mvc;

namespace IronPDFIssue.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    public TestController()
    {
    }

    public async Task<FileResult> CreatePDFAsync()
    {
        var renderer = new ChromePdfRenderer();

        renderer.RenderingOptions.CssMediaType = IronPdf.Rendering.PdfCssMediaType.Print;

        renderer.RenderingOptions.PrintHtmlBackgrounds = true;
        renderer.RenderingOptions.CreatePdfFormsFromHtml = false;

        renderer.RenderingOptions.ViewPortWidth = 1080;
        renderer.RenderingOptions.RenderDelay = 100;

        using var pdfDocument = await renderer.RenderHtmlAsPdfAsync("<html><body><h1>Hello</h1>World</body></html>");

        return File(pdfDocument.Stream, "application/pdf");
    }
}

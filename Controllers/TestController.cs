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

    public FileResult CreatePDF()
    {

        IronPdf.License.LicenseKey = "IRONPDF.DEVTEAM.IX9486-A7AE61CF7A-BKPEIB4GR4-WGFX6QMIVAT3-YF3XNAOCFJDN-C67G6D2TIBDS-CWSGCEM3FDQX-CSIXJY-LXIKP4RCDUDOUA-UNIT.TESTING-5KFMOT.RENEW.SUPPORT.23.JAN.2103";

        IronPdf.Logging.Logger.EnableDebugging = true;

        IronPdf.Installation.TempFolderPath = "./temp";
        Installation.Initialize();
        IronPdf.Installation.SkipShutdown = true;
        ChromePdfRenderer renderer = new ChromePdfRenderer();
        renderer.RenderingOptions.CssMediaType = IronPdf.Rendering.PdfCssMediaType.Print;

        renderer.RenderingOptions.PrintHtmlBackgrounds = true;
        renderer.RenderingOptions.CreatePdfFormsFromHtml = false;

        renderer.RenderingOptions.ViewPortWidth = 1080;
        renderer.RenderingOptions.RenderDelay = 100;

        PdfDocument document = null;
        for (int i = 0; i < 15; i++)
        {
            document = renderer.RenderUrlAsPdf("https://www.google.com");
        }
        Installation.Cleanup();
        return File(document.Stream, "application/pdf");
    }
}

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
        Program.eventStartRender.Set();
        Program.eventDoneRender.WaitOne();

        return File(Program.data, "application/pdf");
    }
}

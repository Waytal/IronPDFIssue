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
        Program.eventStartRender.Set();
        Program.eventDoneRender.WaitOne();

        return File(Program.data, "application/pdf");
    }
}

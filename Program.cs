using IronPdf;
public class Program
{
    public static byte[] data = null;
    public static AutoResetEvent eventStartRender = new AutoResetEvent(false);
    public static AutoResetEvent eventDoneRender = new AutoResetEvent(false);
    static void IronPdfThread()
    {
        // configure initialization settings
        IronPdf.Logging.Logger.EnableDebugging = true;
        IronPdf.Logging.Logger.LogFilePath = "Default.log";
        IronPdf.Logging.Logger.LoggingMode = IronPdf.Logging.Logger.LoggingModes.All;
        IronPdf.Installation.LicenseKey = "---";
        Installation.LinuxAndDockerDependenciesAutoConfig = false;
        Installation.ChromeGpuMode = IronPdf.Engines.Chrome.ChromeGpuModes.Disabled;
        // configure renderer settings
        var renderer = new IronPdf.ChromePdfRenderer();
        renderer.RenderingOptions.CssMediaType = IronPdf.Rendering.PdfCssMediaType.Print;
        renderer.RenderingOptions.PrintHtmlBackgrounds = true;
        renderer.RenderingOptions.CreatePdfFormsFromHtml = false;
        renderer.RenderingOptions.ViewPortWidth = 1080;
        renderer.RenderingOptions.RenderDelay = 100;
        while (true)
        {
            // wait for call
            eventStartRender.WaitOne();
            // render
            using var pdfDocument = renderer.RenderHtmlAsPdf("<html><body><h1>Hello</h1>World</body></html>");
            // set data
            data = pdfDocument.BinaryData;
            // signal done
            eventDoneRender.Set();
        }
    }

    public static void Main(string[] args)
    {
        // NOTE: On MacOs, Chrome requires that the main UI thread ID never changes! Chrome uses fundamentally different threading code for MacOs vs Linux or Windows
        // ...so MacOs ASP.NET requires a persistent thread for Chrome (otherwise the thread ID changes periodically in asp.net)
        // ...this is only an issue on MacOs using ASP.NET and does not occur on Linux (in any circumstance), Windows (in any circumstance), or on MacOs when not using ASP.NET

        // start thread
        Thread thread = new Thread(new ThreadStart(IronPdfThread));
        thread.Priority = ThreadPriority.Highest;
        thread.Start();
            var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
        builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

    // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        var ironPdfLicenseKey = app.Configuration.GetSection("IronPdf").GetValue<string>("LicenseKey");

        IronPdf.License.LicenseKey = ironPdfLicenseKey;

        IronPdf.Logging.Logger.EnableDebugging = true;

        IronPdf.Installation.TempFolderPath = "./temp";

        IronPdf.Installation.Initialize();

        app.Run();
    }
}
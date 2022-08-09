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

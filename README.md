## About this Repo

I'm working on a backend server that integrates IronPDF. 

I encountered a problem with the latest version of the nuget package, on my local machine.
The computer is a MacBook Pro 2019 (intel chip) on macOS Monterey 12.4

You'll find in this repository a small project wich encountered the same problem as my main project.

It might be related to the new disposable PDFDocument class or to something I missed ?

### The big problem

When I call the api endpoint to render my file it works the first time, but fails the second and all other times.


## Implementation

### Iron PDF usage

```cs
IronPdf.Logging.Logger.EnableDebugging = true;

IronPdf.Installation.Initialize();
```

```cs
using var pdfDocument = await renderer.RenderHtmlAsPdfAsync("<html><body><h1>Hello</h1>World</body></html>");

return File(pdfDocument.Stream, "application/pdf");

```

### Log files

I kept the log files of my test in the repo, feel free to check them.

- [cef.log](./cef.log)

- [IronSoftware.ChromeRenderer.log](./IronSoftware.ChromeRenderer.log)

- [IronSoftware.ChromeRenderer.Subprocesses.log](./IronSoftware.ChromeRenderer.Subprocesses.log)

### Nuget packages

```xml
<PackageReference Include="IronPdf.Native.Chrome.MacOS" Version="2022.6.6072" />
<PackageReference Include="IronPdf" Version="2022.6.6115" />
```

## Start the project

You need to have installed [DotNetCore 6](https://dotnet.microsoft.com/en-us/download) and [VSCode](https://code.visualstudio.com/).

Then on your local machine, open the project with VScode install the C# extension and you should be good to go. 

Press  <kbd>F5</kbd> to start the project.
using System.Net.Sockets;
using EpubReaderB;

Task<bool>? task = args.Length > 0 ? EpubInfo.InitializeBook(new FileInfo(args.FirstOrDefault(s => s.EndsWith(".epub"), args[0]))) : null;

if (Environment.ProcessPath is string processPath)
    if (new FileInfo(processPath).DirectoryName is string dir)
        Environment.CurrentDirectory = dir;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLocalization(options => options.ResourcesPath = "Localization");

// Add services to the container.
builder.Services.AddControllersWithViews().AddViewLocalization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.UseRequestLocalization(options =>
{
    var cultures = Config.Cultures;
    options.AddSupportedCultures(cultures);
    options.AddSupportedUICultures(cultures);
    options.SetDefaultCulture(cultures[0]);

    options.ApplyCurrentCultureToResponseHeaders = true;
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

var port = Utils.GetPort(Config.PerferredPort);

Console.WriteLine("EpubReaderB is available at:");
Console.ForegroundColor = ConsoleColor.Green;
foreach (var ip in Utils.GetAllOperationalIPs())
    if (ip.AddressFamily == AddressFamily.InterNetworkV6)
        Console.WriteLine($"  http://[{ip}]:{port}");
    else
        Console.WriteLine($"  http://{ip}:{port}");
Console.WriteLine($"  http://localhost:{port}");
Console.ResetColor();

if (task != null) await task;

app.Run($"http://+:{port}");

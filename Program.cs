using EpubReaderB;

Task<bool>? task = args.Length > 0 ? EpubInfo.InitializeBook(new FileInfo(args[0])) : null;

if (Environment.ProcessPath is string processPath)
    if (new FileInfo(processPath).DirectoryName is string dir)
        Environment.CurrentDirectory = dir;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

if (task != null) await task;

app.Run();

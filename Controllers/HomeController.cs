using EpubReaderB.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EpubReaderB.Controllers;

public class HomeController(ILogger<HomeController> logger) : Controller
{
    private readonly ILogger<HomeController> _logger = logger;

    public IActionResult Index() => View(new ReaderViewModel(EpubInfo.EpubBook));

    public IActionResult Cover() => View(new CoverViewModel(EpubInfo.EpubBook));

    public IActionResult NavPage() => View(new NavPageViewModel(EpubInfo.EpubBook));

    public IActionResult Privacy() => View();

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}

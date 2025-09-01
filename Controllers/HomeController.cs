using EpubReaderB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Diagnostics;

namespace EpubReaderB.Controllers;

public class HomeController(IStringLocalizer<HomeController> sl) : Controller
{
    private readonly IStringLocalizer<HomeController> localizer = sl;

    public IActionResult Index() => View(new ReaderViewModel(EpubInfo.EpubBook, localizer));

    public IActionResult Cover() => View(new CoverViewModel(EpubInfo.EpubBook, localizer));

    public IActionResult NavPage() => View(new NavPageViewModel(EpubInfo.EpubBook, localizer));

    public IActionResult About() => View(new AboutViewModel(EpubInfo.EpubBook, localizer));

    public IActionResult ResourceIndex() => View(new ResourceIndexViewModel(EpubInfo.EpubBook, localizer));

    public IActionResult Gallery() => View(new GalleryViewModel(EpubInfo.EpubBook, localizer));

    public IActionResult Privacy() => View();

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}

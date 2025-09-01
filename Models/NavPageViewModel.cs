using EpubReaderB.Controllers;
using Microsoft.Extensions.Localization;
using VersOne.Epub;

namespace EpubReaderB.Models;

public class NavPageViewModel(EpubBook? book, IStringLocalizer<HomeController> sl) : BookViewModelBase(book, sl)
{
    public new string Title => $"{localizer["Navigation"]} - {base.Title}";
}

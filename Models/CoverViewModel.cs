using EpubReaderB.Controllers;
using Microsoft.Extensions.Localization;
using VersOne.Epub;

namespace EpubReaderB.Models;

public class CoverViewModel(EpubBook? book, IStringLocalizer<HomeController> sl) : BookViewModelBase(book, sl)
{
    public new string Title => $"{localizer["Cover"]} - {base.Title}";
}

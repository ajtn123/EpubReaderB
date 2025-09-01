using EpubReaderB.Controllers;
using Microsoft.Extensions.Localization;
using VersOne.Epub;

namespace EpubReaderB.Models;

public class BookViewModelBase(EpubBook? book, IStringLocalizer<HomeController> sl)
{
    protected IStringLocalizer<HomeController> localizer = sl;

    public EpubBook? Book { get; } = book;

    public string BookTitle => Book?.Title ?? localizer["Untitled"].Value;
    public string Title => Book?.Title ?? localizer["Untitled"].Value;
    public string BookCopyright => Book != null ? $"{Book.Title} © {Book.Author}" : "";
}

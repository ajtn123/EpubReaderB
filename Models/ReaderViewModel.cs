using EpubReaderB.Controllers;
using Microsoft.Extensions.Localization;
using VersOne.Epub;

namespace EpubReaderB.Models;

public class ReaderViewModel(EpubBook? book, IStringLocalizer<HomeController> sl) : BookViewModelBase(book, sl)
{

}

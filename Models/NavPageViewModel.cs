using VersOne.Epub;

namespace EpubReaderB.Models;

public class NavPageViewModel(EpubBook? book) : BookViewModelBase(book)
{
    public new string Title => base.Title + "Navigation - ";
}

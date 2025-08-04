using VersOne.Epub;

namespace EpubReaderB.Models;

public class NavPageViewModel(EpubBook? book) : BookViewModelBase(book)
{
    public new string Title => "Navigation - " + base.Title;
}

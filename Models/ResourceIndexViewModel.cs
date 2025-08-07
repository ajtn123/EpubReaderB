using VersOne.Epub;

namespace EpubReaderB.Models;

public class ResourceIndexViewModel(EpubBook? book) : BookViewModelBase(book)
{
    public new string Title => "Resource Index - " + base.Title;
}

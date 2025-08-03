using VersOne.Epub;

namespace EpubReaderB.Models;

public class CoverViewModel(EpubBook? book) : BookViewModelBase(book)
{
    public new string Title => base.Title + "Cover - ";
}

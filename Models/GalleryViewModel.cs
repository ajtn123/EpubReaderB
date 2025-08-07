using VersOne.Epub;

namespace EpubReaderB.Models;

public class GalleryViewModel(EpubBook? book) : BookViewModelBase(book)
{
    public new string Title => "Gallery - " + base.Title;
}

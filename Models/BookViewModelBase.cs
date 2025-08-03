using VersOne.Epub;

namespace EpubReaderB.Models;

public class BookViewModelBase(EpubBook? book)
{
    public EpubBook? Book { get; set; } = book;
}

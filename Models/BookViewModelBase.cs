using VersOne.Epub;

namespace EpubReaderB.Models;

public class BookViewModelBase(EpubBook? book)
{
    public EpubBook? Book { get; set; } = book;

    public string BookTitle { get; set; } = book?.Title ?? "Untitled";
    public string Title { get; set; } = book?.Title ?? "Untitled";
    public string BookCopyright { get; set; } = book != null ? $"{book.Title} © {book.Author}" : "";
}

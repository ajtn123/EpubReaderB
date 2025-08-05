using VersOne.Epub;

namespace EpubReaderB.Models;

public class BookViewModelBase(EpubBook? book)
{
    public EpubBook? Book { get; } = book;

    public string BookTitle { get; } = book?.Title ?? "Untitled";
    public string Title { get; } = book?.Title ?? "Untitled";
    public string BookCopyright { get; } = book != null ? $"{book.Title} © {book.Author}" : "";
}

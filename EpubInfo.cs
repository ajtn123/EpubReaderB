using EpubReaderB.Controllers;
using VersOne.Epub;

namespace EpubReaderB;

public class EpubInfo : IDisposable
{
    public static async Task<bool> InitializeBook(FileInfo file)
    {
        EpubInfo epubInfo = new() { File = file };

        Stream? stream = null;
        if (epubInfo.File != null && epubInfo.File.Exists)
            try
            {
                Console.WriteLine($"Reading file: {epubInfo.File.FullName}");
                stream = new MemoryStream(await System.IO.File.ReadAllBytesAsync(epubInfo.File.FullName));
            }
            catch (Exception e) { Console.WriteLine(e.Message); }

        if (stream != null) return await InitializeBook(stream);
        else return epubInfo.IsInitialized = false;
    }

    public static async Task<bool> InitializeBook(Stream stream, EpubInfo? epubInfo = null)
    {
        epubInfo ??= new();
        epubInfo.Stream = stream;

        if (epubInfo.Stream != null)
            try
            {
                epubInfo.Book = await EpubReader.ReadBookAsync(epubInfo.Stream.Clone());
            }
            catch (Exception e) { Console.WriteLine(e.Message); }

        if (epubInfo.Book == null || epubInfo.Stream == null)
            return epubInfo.IsInitialized = false;

        CurrentBook?.Dispose();

        CurrentBook = epubInfo;

        _ = ResourceController.AddResAll(epubInfo.Book);

        Utils.Deobfuscate(epubInfo.Book, epubInfo.Stream);

        if (System.IO.File.Exists("styles.css"))
            Styles = await System.IO.File.ReadAllTextAsync("styles.css");
        else Styles = "";

        return epubInfo.IsInitialized = true;
    }

    public void Dispose()
    {
        Stream?.Dispose();
        GC.SuppressFinalize(this);
    }

    public bool IsInitialized { get; private set; } = false;

    public FileInfo? File { get; set; }
    public Stream? Stream { get; set; }
    public EpubBook? Book { get; set; }

    public static FileInfo? EpubFile => CurrentBook?.File;
    public static Stream? EpubStream => CurrentBook?.Stream;
    public static EpubBook? EpubBook => CurrentBook?.Book;

    public static EpubInfo? CurrentBook { get; private set; }

    public static string Styles { get; set; } = "";
}

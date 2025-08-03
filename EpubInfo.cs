using EpubReaderB.Controllers;
using VersOne.Epub;

namespace EpubReaderB;

public static class EpubInfo
{
    public static async Task<bool> InitializeBook(FileInfo file)
    {
        EpubFile = file;

        Stream? stream = null;
        if (EpubFile != null && EpubFile.Exists)
            try
            {
                Console.WriteLine($"Reading file: {EpubFile.FullName}");
                stream = new MemoryStream(await File.ReadAllBytesAsync(EpubFile.FullName));
            }
            catch (Exception e) { Console.WriteLine(e.Message); }

        if (stream != null) return await InitializeBook(stream);
        else return IsInitialized = false;
    }

    public static async Task<bool> InitializeBook(Stream stream)
    {
        EpubStream?.Dispose();
        EpubStream = stream;

        EpubBook = null;
        if (EpubStream != null)
            try
            {
                EpubBook = await EpubReader.ReadBookAsync(EpubStream.Clone());
            }
            catch (Exception e) { Console.WriteLine(e.Message); }

        if (EpubBook == null || EpubStream == null)
            return IsInitialized = false;

        _ = ResourceController.AddResAll(EpubBook);

        Utils.Deobfuscate(EpubBook, EpubStream);

        return IsInitialized = true;
    }

    public static bool IsInitialized { get; private set; } = false;

    public static FileInfo? EpubFile { get; set; }
    public static Stream? EpubStream { get; set; }
    public static EpubBook? EpubBook { get; set; }
}

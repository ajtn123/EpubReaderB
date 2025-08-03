using EpubReaderB.Controllers;
using EpubReaderB.Models;
using System.IO;
using System.IO.Compression;
using System.Net.WebSockets;
using VersOne.Epub;

namespace EpubReaderB;

public static class EpubInfo
{
    public static void InitializeBook(FileInfo file)
    {
        EpubFile = file;

        EpubStream?.Dispose();
        EpubStream = null;
        if (EpubFile != null && EpubFile.Exists)
            try
            {
                Console.WriteLine($"Reading file: {EpubFile.FullName}");
                EpubStream = new MemoryStream(File.ReadAllBytes(EpubFile.FullName));
            }
            catch (Exception e) { Console.WriteLine(e.Message); }

        EpubBook = null;
        if (EpubStream != null)
            try
            {
                EpubBook = EpubReader.ReadBook(EpubStream.Clone());
            }
            catch (Exception e) { Console.WriteLine(e.Message); }

        if (EpubBook == null || EpubStream == null) return;

        _ = ResourceController.AddResAll(EpubBook);

        Utils.Deobfuscate(EpubBook, EpubStream);

        IsInitialized = true;
    }

    public static bool IsInitialized { get; private set; } = false;

    public static FileInfo? EpubFile { get; set; }
    public static Stream? EpubStream { get; set; }
    public static EpubBook? EpubBook { get; set; }
}

using EpubReaderB.Controllers;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;
using VersOne.Epub;

namespace EpubReaderB.Models;

public class ReaderViewModel
{
    public EpubBook? Book { get; set; }
    public ReaderViewModel(string? path)
    {
        if (string.IsNullOrWhiteSpace(path)) return;
        Console.WriteLine(path);
        try
        {
            Book = EpubReader.ReadBook(path);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return;
        }

        foreach (var la in Book.Content.AllFiles.Local)
            if (la is EpubLocalTextContentFile t) AddRes(t);
            else if (la is EpubLocalByteContentFile b) AddRes(b);

        Deobfuscate(Book);
    }

    private static void AddRes(EpubLocalTextContentFile? file)
    {
        if (file is null) return;
        ResourceController.AddFile(file.Key, file.Content, file.ContentMimeType);
    }

    private static void AddRes(EpubLocalByteContentFile? file)
    {
        if (file is null) return;
        ResourceController.AddFile(file.Key, file.Content, file.ContentMimeType);
    }

    private static void Deobfuscate(EpubBook book)
    {
        var path = book?.FilePath;
        if (path == null) return;

        using var zip = ZipFile.OpenRead(path);
        var entry = zip.GetEntry("META-INF/encryption.xml");

        if (entry == null)
            Console.WriteLine("encryption.xml not found.");
        else
        {
            var id = book?.Schema.Package.Metadata.Identifiers
                .FirstOrDefault(a => a?.Id == book.Schema.Package.UniqueIdentifier, null)?
                .Identifier.Replace(" ", "");
            if (id == null) return;
            var key = SHA1.HashData(Encoding.UTF8.GetBytes(id));

            using var reader = new StreamReader(entry.Open());

            var xml = XDocument.Load(reader);
            XNamespace enc = "http://www.w3.org/2001/04/xmlenc#";

            var fontUris = xml.Descendants(enc + "EncryptedData")
                .Where(ed =>
                    (string?)ed.Element(enc + "EncryptionMethod")?.Attribute("Algorithm") ==
                    "http://www.idpf.org/2008/embedding")
                .Select(ed =>
                    (string?)ed.Element(enc + "CipherData")?.Element(enc + "CipherReference")?.Attribute("URI"))
                .Where(uri => !string.IsNullOrEmpty(uri))
                .ToList();

            var rootPath = book?.Schema.ContentDirectoryPath;
            if (rootPath == null) return;

            foreach (var fontUri in fontUris)
            {
                Console.WriteLine($"Deobfuscating: {fontUri}");
                if (string.IsNullOrWhiteSpace(fontUri)) return;

                using var ze = zip.GetEntry(fontUri)?.Open();
                if (ze == null) return;
                using var input = new MemoryStream();
                ze.CopyTo(input);
                input.Position = 0;

                using MemoryStream output = new();

                const int maxBytes = 1040;
                var keyLen = key.Length;
                var processed = 0;

                while (processed < maxBytes && input.Position < input.Length)
                {
                    for (var i = 0; i < keyLen && processed < maxBytes && input.Position < input.Length; i++)
                    {
                        var b = input.ReadByte();
                        if (b == -1) break;

                        var deobfuscated = (byte)(b ^ key[i]);
                        output.WriteByte(deobfuscated);
                        processed++;
                    }
                }

                input.CopyTo(output);

                ResourceController.AddFile(Path.GetRelativePath(rootPath, fontUri), output.ToArray());
            }
        }
    }
}

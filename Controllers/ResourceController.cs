using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using VersOne.Epub;

namespace EpubReaderB.Controllers;

public class ResourceController : Controller
{
    private static readonly Dictionary<string, (byte[] Content, string MimeType)> _files = [];

    [HttpGet("/resources/{*name}")]
    public IActionResult Get(string name)
    {
        if (_files.TryGetValue(name, out var file))
            return File(file.Content, file.MimeType);
        if (Uri.TryCreate(name, UriKind.Absolute, out _))
            return Redirect(name);
        else
            return NotFound();
    }

    public static void AddFile(string name, byte[] content, string? mimeType = null)
        => _files[name] = (content, string.IsNullOrWhiteSpace(mimeType) ? GetMimeType(name) : mimeType);
    public static void AddFile(string name, string content, string? mimeType = null)
        => AddFile(name, System.Text.Encoding.UTF8.GetBytes(content), mimeType);

    public static void AddRes(EpubLocalTextContentFile file)
        => AddFile(file.Key, file.Content, file.ContentMimeType);
    public static void AddRes(EpubLocalByteContentFile file)
        => AddFile(file.Key, file.Content, file.ContentMimeType);

    public static bool AddResAll(EpubBook book)
    {
        foreach (var res in book.Content.AllFiles.Local)
            if (res is EpubLocalTextContentFile t) AddRes(t);
            else if (res is EpubLocalByteContentFile b) AddRes(b);
        return true;
    }

    private static readonly FileExtensionContentTypeProvider _typeProvider = new();
    private static string GetMimeType(string fileName) =>
        _typeProvider.TryGetContentType(fileName, out var mimeType)
            ? mimeType
            : "application/octet-stream";
}

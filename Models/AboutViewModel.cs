using System.Text.Json;
using VersOne.Epub;

namespace EpubReaderB.Models;

public class AboutViewModel(EpubBook? book) : BookViewModelBase(book)
{
    public new string Title => "About - " + base.Title;

    public string PackageInfo { get; set; } = JsonSerializer.Serialize(book?.Schema.Package, Utils.JsonSerializerOptions);
    public string Rights { get; set; } = string.Join('\n', book?.Schema.Package.Metadata.Rights.Select(x => x.Rights) ?? []);
    public string Publishers { get; set; } = string.Join(", ", book?.Schema.Package.Metadata.Publishers.Select(x => x.Publisher) ?? []);
}

using System.Text.Json;
using EpubReaderB.Controllers;
using Microsoft.Extensions.Localization;
using VersOne.Epub;

namespace EpubReaderB.Models;

public class AboutViewModel(EpubBook? book, IStringLocalizer<HomeController> sl) : BookViewModelBase(book, sl)
{
    public new string Title => $"{localizer["About"]} - {base.Title}";

    public string PackageInfo { get; } = JsonSerializer.Serialize(book?.Schema.Package, Utils.JsonSerializerOptions);
    public string Rights { get; } = string.Join('\n', book?.Schema.Package.Metadata.Rights.Select(x => x.Rights) ?? []);
    public string Publishers { get; } = string.Join(", ", book?.Schema.Package.Metadata.Publishers.Select(x => x.Publisher) ?? []);
}

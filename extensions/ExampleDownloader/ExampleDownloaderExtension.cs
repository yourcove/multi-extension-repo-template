using Cove.Plugins;
using Microsoft.Extensions.DependencyInjection;

namespace ExampleDownloader;

public sealed class ExampleDownloaderExtension : IDownloaderProvider
{
    public string Id => "com.example.downloader";
    public string Name => "Example Downloader";
    public string Version => "0.1.0";
    public string? Description => "A minimal downloader extension template.";
    public string? Author => "Example Author";
    public string? Url => "https://github.com/example/cove-extensions";
    public string? IconUrl => null;
    public IReadOnlyList<string> Categories => [ExtensionCategories.Downloader, ExtensionCategories.Metadata, ExtensionCategories.Integration];
    public string? MinCoveVersion => "1.0.0";

    public void ConfigureServices(IServiceCollection services, ExtensionContext context)
    {
    }

    public IReadOnlyList<DownloaderDescriptor> GetDownloaders() =>
    [
        new(
            "com.example.downloader.file",
            "Example downloader",
            DownloaderEntity.Scene,
            ["https://example.com/*"],
            DownloaderCapabilities.InlineMetadata)
    ];

    public Task<DownloaderUrlMatch?> MatchAsync(string url, CancellationToken ct)
    {
        if (!Uri.TryCreate(url, UriKind.Absolute, out var uri))
            return Task.FromResult<DownloaderUrlMatch?>(null);

        if (!string.Equals(uri.Host, "example.com", StringComparison.OrdinalIgnoreCase))
            return Task.FromResult<DownloaderUrlMatch?>(null);

        return Task.FromResult<DownloaderUrlMatch?>(new DownloaderUrlMatch(
            "com.example.downloader.file",
            uri.ToString(),
            Label: "Example media"));
    }
}
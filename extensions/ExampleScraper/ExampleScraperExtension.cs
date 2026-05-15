using Cove.Core.DTOs;
using Cove.Plugins;
using Microsoft.Extensions.DependencyInjection;

namespace ExampleScraper;

public sealed class ExampleScraperExtension : IScraperProvider
{
    public string Id => "com.example.scraper";
    public string Name => "Example Scraper";
    public string Version => "0.1.0";
    public string? Description => "A minimal compiled scraper extension template.";
    public string? Author => "Example Author";
    public string? Url => "https://github.com/example/cove-extensions";
    public string? IconUrl => null;
    public IReadOnlyList<string> Categories => [ExtensionCategories.Scraper, ExtensionCategories.Metadata, ExtensionCategories.Integration];
    public string? MinCoveVersion => "1.0.0";

    public void ConfigureServices(IServiceCollection services, ExtensionContext context)
    {
    }

    public IReadOnlyList<ScraperDescriptor> GetScrapers() =>
    [
        new(
            "com.example.scraper.scene",
            "Example scene scraper",
            ScraperEntity.Scene,
            ScraperCapabilities.ByUrl | ScraperCapabilities.ByName,
            ["example.com/videos/", "media.example.com/watch/"])
    ];

    public Task<ScrapedSceneDto?> ScrapeSceneAsync(ScraperRequest<SceneScrapeInput> request, CancellationToken ct)
    {
        var sourceUrl = request.Input.Url ?? request.Input.Urls.FirstOrDefault();
        if (!TryCreateSupportedUri(sourceUrl, out var uri))
            return Task.FromResult<ScrapedSceneDto?>(null);

        var title = string.IsNullOrWhiteSpace(request.Input.Title)
            ? "Example scene"
            : request.Input.Title.Trim();

        return Task.FromResult<ScrapedSceneDto?>(new ScrapedSceneDto
        {
            Title = title,
            Code = request.Input.Code ?? Path.GetFileName(uri.AbsolutePath),
            Date = request.Input.Date,
            Details = request.Input.Details,
            Urls = [uri.ToString()],
            StudioName = "Example Studio",
            TagNames = ["example"],
        });
    }

    public Task<IReadOnlyList<ScrapedSceneDto>> SearchScenesAsync(ScraperRequest<string> request, CancellationToken ct)
    {
        var query = request.Input?.Trim();
        if (string.IsNullOrWhiteSpace(query))
            return Task.FromResult<IReadOnlyList<ScrapedSceneDto>>([]);

        var slug = Uri.EscapeDataString(query.ToLowerInvariant().Replace(' ', '-'));
        IReadOnlyList<ScrapedSceneDto> results =
        [
            new ScrapedSceneDto
            {
                Title = query,
                Urls = [$"https://example.com/videos/{slug}"],
                StudioName = "Example Studio",
                TagNames = ["example"],
            },
        ];

        return Task.FromResult(results);
    }

    private static bool TryCreateSupportedUri(string? url, out Uri uri)
    {
        if (Uri.TryCreate(url, UriKind.Absolute, out uri!)
            && (string.Equals(uri.Host, "example.com", StringComparison.OrdinalIgnoreCase)
                || string.Equals(uri.Host, "media.example.com", StringComparison.OrdinalIgnoreCase)))
        {
            return true;
        }

        uri = null!;
        return false;
    }
}

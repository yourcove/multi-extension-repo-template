# Cove Multi-Extension Template

Use this template when one repository owns multiple related Cove extensions. On
GitHub, create new extension sets with the **Use this template** button.

After creating a repository from the template, replace the example extension IDs,
namespaces, manifest fields, catalog entries, and workflow matrix values with
your real extension set.

## Build

```powershell
dotnet build -c Release
```

## Release tags

Each extension has its own tag prefix:

- `example-ui/v0.1.0`
- `example-downloader/v0.1.0`
- `example-scraper/v0.1.0`

The CI workflow only packages the extension that matches the pushed tag.

## Scrapers

`extensions/ExampleScraper` is a compiled `IScraperProvider` example for
scrapers that need C# logic. For script-free XPath scrapers, see the pure YAML
example in `scraper-examples/pure-yaml/ExampleVideo.yml`.

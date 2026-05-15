using Cove.Plugins;
using Microsoft.Extensions.DependencyInjection;

namespace ExampleUi;

public sealed class ExampleUiExtension : IExtension
{
    public string Id => "com.example.ui";
    public string Name => "Example UI Extension";
    public string Version => "0.1.0";
    public string? Description => "A minimal UI extension template.";
    public string? Author => "Example Author";
    public string? Url => "https://github.com/example/cove-extensions";
    public string? IconUrl => null;
    public IReadOnlyList<string> Categories => [ExtensionCategories.UI, ExtensionCategories.Tools];
    public string? MinCoveVersion => "1.0.0";

    public void ConfigureServices(IServiceCollection services, ExtensionContext context)
    {
    }
}
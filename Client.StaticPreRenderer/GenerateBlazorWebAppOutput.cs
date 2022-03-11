using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Client.StaticPreRenderer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

public class GenerateBlazorWebAppOutput : IClassFixture<BlazorWebAppTestFixture>
{
    private readonly BlazorWebAppTestFixture _fixture;
    private readonly HttpClient _client;
    private readonly string _outputPath;

    public GenerateBlazorWebAppOutput(BlazorWebAppTestFixture fixture)
    {
        _fixture = fixture;
        
        _client = fixture.CreateDefaultClient();

        var config = _fixture.Services.GetRequiredService<IConfiguration>();
        
        _outputPath = config["RenderOutputDirectory"];
    }

    [Theory, Trait("Category", "PreRender")]
    [InlineData("/")]
    [InlineData("/counter")]
    [InlineData("/fetchdata")]
    public async Task Render(string route)
    {
        var renderPath = route.Substring(1); // strip the initial / off

        var relativePath = Path.Combine(_outputPath, renderPath); // create the output directory
        
        var outputDirectory = Path.GetFullPath(relativePath);
        
        Directory.CreateDirectory(outputDirectory);

        var filePath = Path.Combine(outputDirectory, "index.html"); // Build the output file path
        
        var result = await _client.GetStreamAsync(route); // Call the prerendering API, and write the contents to the file
        
        using (var file = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
        {
            await result.CopyToAsync(file);
        }
    }
}
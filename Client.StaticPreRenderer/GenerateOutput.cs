using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Client.StaticPreRenderer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

public class GenerateOutput : IClassFixture<AppTestFixture>
{
    private readonly AppTestFixture _fixture;
    private readonly HttpClient _client;
    private readonly string _outputPath;

    public GenerateOutput(AppTestFixture fixture)
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
        // strip the initial / off
        var renderPath = route.Substring(1);

        // create the output directory
        var relativePath = Path.Combine(_outputPath, renderPath);
        var outputDirectory = Path.GetFullPath(relativePath);
        Directory.CreateDirectory(outputDirectory);

        // Build the output file path
        var filePath = Path.Combine(outputDirectory, "index.html");

        // Call the prerendering API, and write the contents to the file
        var result = await _client.GetStreamAsync(route);
        
        using (var file = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
        {
            await result.CopyToAsync(file);
        }
    }
}
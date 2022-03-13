using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Client.StaticPreRenderer;
using Client.StaticPreRenderer.Factories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

public class GenerateBlazorWebAppOutput : IClassFixture<WebAppTestFixture>
{
    private readonly WebAppTestFixture _fixture;
    private readonly HttpClient _client;
    private readonly string _outputPath;

    public GenerateBlazorWebAppOutput(WebAppTestFixture fixture)
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
    [InlineData("/Blogs/blog1")]
    public async Task Render(string route)
    {
        var getContentTask = _client.GetStreamAsync(route); // Call the prerendering API, and write the contents to the file

        var factory = new BlazorToStaticPageFactory();

        await factory.Create(await getContentTask, route, _outputPath);
    }
}
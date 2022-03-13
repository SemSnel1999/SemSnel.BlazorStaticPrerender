using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Client.StaticPreRenderer.Factories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Client.StaticPreRenderer;

public class GenerateServerOutput : IClassFixture<WebAppTestFixture>
{
    private readonly WebAppTestFixture _fixture;
    private readonly HttpClient _client;
    private readonly string _outputPath;

    public GenerateServerOutput(WebAppTestFixture fixture)
    {
        _fixture = fixture;
        
        _client = fixture.CreateDefaultClient();

        var config = _fixture.Services.GetRequiredService<IConfiguration>();
        
        _outputPath = config["RenderOutputDirectory"];
    }
    
    [Theory, Trait("Category", "PreRender")]
    [InlineData("/WeatherForecast")]
    public async Task Render(string route)
    {
        var result = await _client.GetStreamAsync($"/api{route}"); // Call the prerendering API, and write the contents to the file

        var factory = new ServerResponseToStaticFileFactory();

        await factory.Create(result, route, _outputPath);
    }
}
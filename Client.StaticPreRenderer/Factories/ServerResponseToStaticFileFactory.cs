using System.IO;
using System.Threading.Tasks;
using Client.StaticPreRenderer.Extensions;

namespace Client.StaticPreRenderer.Factories;

public class ServerResponseToStaticFileFactory
{
    public async Task Create(Stream content, string route, string outputPath)
    {
        var dataPath = "data-collections"; // strip the initial / off

        var contentPath = Path.Combine(dataPath, "content");

        var segments = route.GetRouteSegments();
        
        var relativePath = Path.Combine(outputPath, contentPath);
        
        var outputDirectory = Path.GetFullPath(relativePath);
        
        foreach (var segment in segments)
        {
            relativePath = Path.Combine(relativePath, segment);
            
            outputDirectory = Path.GetFullPath(relativePath);

            Directory.CreateDirectory(outputDirectory);
        }
        
        var filePath = Path.Combine(outputDirectory, "data.json"); // Build the output file path
        
        using (var file = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
        {
            await content.CopyToAsync(file);
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Client.StaticPreRenderer.Extensions;

namespace Client.StaticPreRenderer.Factories;

public class BlazorToStaticPageFactory
{
    public async Task Create(Stream content, string route, string outputPath)
    {
        var segments = route.GetRouteSegments();
        
        var relativePath = outputPath;
        
        var outputDirectory = Path.GetFullPath(relativePath);
        
        foreach (var segment in segments)
        {
            relativePath = Path.Combine(relativePath, segment);
            
            outputDirectory = Path.GetFullPath(relativePath);

            Directory.CreateDirectory(outputDirectory);
        }
        
        var filePath = Path.Combine(outputDirectory, "index.html"); // Build the output file path

        using (var file = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
        {
            await content.CopyToAsync(file);
        }
    }
}
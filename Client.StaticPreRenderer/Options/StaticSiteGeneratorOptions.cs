using System.IO;

namespace Client.StaticPreRenderer.Options;

public class StaticSiteGeneratorOptions
{
    public string OutputDirectory { get; set; }
    public string DataOutputPath { get; set; } = "data-collections";
}
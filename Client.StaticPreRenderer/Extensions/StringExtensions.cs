using System.Collections.Generic;
using System.Linq;

namespace Client.StaticPreRenderer.Extensions;

public static class StringExtensions
{
    public static IEnumerable<string> GetRouteSegments(this string route)
    {
        var segments = route
            .Split("/")
            .Select(t => t.Replace("/", ""))
            .Where(t => t.Length > 0);

        if (segments.Count() > 1)
        {
            return segments;
        }

        return new[] {route.Replace("/", "")};
    }
}
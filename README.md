# SemSnel.BlazorStaticPrerender

**Static Blazor if you don't want to host your webapp on a expensive server**

to run the static app got to the "output/wwwroot" folder and use the following command:


dotnet serve -o -S

**Why not use reflection for determining the existing pages?**

I developed this solution to try if I could use this method in combination with a headless content management system. For my use case it is not needed to reflect the blazor page components for the existing urls. The content management system will provide the needed urls needed to determine the existing pages and coupled json data.

**Why even consider using this solution?**

In case the website is expanded and expects more dynamic content, the previously created project can still be used. The hosting server should be modified, however the code will not be affected by the hosting environments.

**Why would I use this method instead of a Azure hosted Host Client Prerenderer?**
- Save unneeded cost
- Improve performance


**When shouldn't I use this method instead of a Azure hosted Host Client Prerenderer?**
- Fast publishing and updating content is not possible
- Dynamicly retrieving data from a server that is prone to changes is not possible


sources:

Lock, A. (2022, 8 maart). Prerendering a Blazor WebAssembly app to static files, without an ASP.NET Core host app. Andrew Lock | .NET Escapades. Geraadpleegd op 9 maart 2022, van https://andrewlock.net/prerending-a-blazor-webassembly-app-without-an-asp-net-core-host-app/

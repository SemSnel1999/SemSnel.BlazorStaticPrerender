# SemSnel.BlazorStaticPrerender

**Static Blazor if you don't want to host your webapp on a expensive server**

to run the static app got to the "output/wwwroot" folder and use the following command:


dotnet serve -o -S

**Why not use reflection for determining the existing pages?**
I developed this solution to try if I could use this method in combination with a headless content management system. For my use case it is not needed to reflect the blazor page components for the existing urls. The content management system will provide the needed urls needed to determine the existing pages and coupled json data.


sources:
https://andrewlock.net/prerending-a-blazor-webassembly-app-without-an-asp-net-core-host-app/

using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Xunit.Abstractions;


namespace Client.StaticPreRenderer;

public class WebAppTestFixture : WebApplicationFactory<Client.Host.Program>
{
    protected override IHostBuilder CreateHostBuilder()
    {
        var builder = base.CreateHostBuilder();
        builder.UseEnvironment(Environments.Production);

        // Replace the HttpClient dependency with a new one
        builder.ConfigureWebHost(
            webHostBuilder =>
            {
                webHostBuilder.UseStaticWebAssets(); // Add this line
                webHostBuilder.ConfigureTestServices(services =>
                {
                    services.Remove(new ServiceDescriptor(typeof(HttpClient), typeof(HttpClient), ServiceLifetime.Singleton));
                    services.AddSingleton(_ => CreateDefaultClient());
                });
            });
        
        return builder;
    }
}
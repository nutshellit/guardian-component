using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using GuardianComponent;

namespace BlazorHosted;
public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        var config = builder.Configuration["IsBlazorHosted"];
        if (config == "True")
        {
            builder.RootComponents.Add<App>("#app");

        }
        builder.RootComponents.Add<HeadOutlet>("head::after");

        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
        var key = builder.Configuration["GuardianKey"];
        builder.Services.AddGuardianServices(key);
        await builder.Build().RunAsync();
    }
}

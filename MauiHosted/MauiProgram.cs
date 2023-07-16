using MauiHosted.Data;
using Microsoft.Extensions.Logging;
using GuardianComponent;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace MauiHosted;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        var a = Assembly.GetExecutingAssembly();
        using var stream = a.GetManifestResourceStream("MauiHosted.appsettings.json");

        var config = new ConfigurationBuilder()
            .AddJsonStream(stream)
            .Build();


        builder.Configuration.AddConfiguration(config);

        builder.Services.AddMauiBlazorWebView();
        var key = config["GuardianKey"];

        builder.Services.AddGuardianServices(key);

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

        builder.Services.AddSingleton<WeatherForecastService>();

        return builder.Build();
    }
}

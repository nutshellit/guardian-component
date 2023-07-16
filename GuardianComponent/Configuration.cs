using GuardianComponent.Features.Guardian;
using Microsoft.Extensions.DependencyInjection;

namespace GuardianComponent;
public static class Configuration
{
    public static IServiceCollection AddGuardianServices(this IServiceCollection services, string guardianKey)
    {
        services.AddHttpClient("guardian", client =>
        {
            client.BaseAddress = new Uri("https://content.guardianapis.com/");
        });
        services.AddSingleton(new GuardianSettings(guardianKey));
        services.AddScoped<IGuardianQuery, GuardianQuery>();
        return services;
    }
}

public record GuardianSettings(string key); 
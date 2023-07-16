using GuardianComponent.Features.Guardian;
using Microsoft.Extensions.DependencyInjection;

namespace GuardianComponent;
public static class Configuration
{
    public static IServiceCollection AddGuardianServices(this IServiceCollection services)
    {
        services.AddHttpClient("guardian", client =>
        {
            client.BaseAddress = new Uri("https://content.guardianapis.com/");
        });
        services.AddScoped<IGuardianQuery, GuardianQuery>();
        return services;
    }
}
using ANetD.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ANetD.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddANetD(this IServiceCollection services)
    {
        services.AddScoped<IANetDJSInterop, ANetDJSInterop>();
        services.AddScoped<IToastService, ToastService>();
        services.AddScoped<ThemeService>();
        return services;
    }
}

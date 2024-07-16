using Application.Services;
using Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Domain.Interfaces;

namespace Application.Extensions;

public static class ServiceExtensions
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddSingleton<IRateCalculator, RateCalculatorService>();
        services.AddSingleton<IRatesLoader, RatesLoaderService>();
    }
}

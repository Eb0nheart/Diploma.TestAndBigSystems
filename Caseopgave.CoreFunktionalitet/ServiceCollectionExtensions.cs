using Caseopgave.Api.Services;
using Caseopgave.CoreFunktionalitet.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Caseopgave.CoreFunktionalitet;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCoreFunctionality(this IServiceCollection services, Action<MotorApiOptions> configure)
    {
        return services
            .AddSingleton<IParkingRepository, ParkingRepository>()
            .AddTransient<IMotorApiFacade, MotorApiFacade>()
            .Configure(configure);
    }
}

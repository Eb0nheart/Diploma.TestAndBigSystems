using Caseopgave.CoreFunktionalitet.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection.Metadata.Ecma335;

namespace Caseopgave.CoreFunktionalitet;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCoreFunctionality(this IServiceCollection services)
        => services.AddSingleton<IParkingRepository, ParkingRepository>();
}

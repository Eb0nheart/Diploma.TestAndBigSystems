namespace Testing.Aflevering.Vejrudsigt;
public static class Extensions
{
    public static IServiceCollection AddFunctionality(this IServiceCollection services)
        => services
            .AddSingleton<IWeatherService, WeatherService>()
            .AddSingleton<IDescriptionGenerator, DescriptionGenerator>();
}
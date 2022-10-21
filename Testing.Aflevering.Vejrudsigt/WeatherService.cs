namespace Testing.Aflevering.Vejrudsigt;

public record WeatherInfo(string Conditions, double Temperature);

public interface IWeatherService
{
    Task<WeatherInfo> GetTodaysWeather(string location);
    Task<WeatherInfo> GetYesterdaysWeather(string location);
}

sealed class WeatherService : IWeatherService
{
    private readonly HttpClient client;
    private readonly IConfiguration configuration;

    public WeatherService(HttpClient client, IConfiguration configuration)
    {
        this.client = client;
        this.configuration = configuration;
    }

    public async Task<WeatherInfo> GetTodaysWeather(string location)
    {
        var url = $"https://smartweatherdk.azurewebsites.net/api/GetTodaysWeather?key={configuration["key"]}&location={location}";
        return await client.GetFromJsonAsync<WeatherInfo>(url);
    }

    public async Task<WeatherInfo> GetYesterdaysWeather(string location)
    {
        var url = $"https://smartweatherdk.azurewebsites.net/api/GetYesterdaysWeather?key={configuration["key"]}&location={location}";
        return await client.GetFromJsonAsync<WeatherInfo>(url);
    }
}

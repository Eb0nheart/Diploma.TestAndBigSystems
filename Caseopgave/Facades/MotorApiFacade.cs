using Microsoft.Extensions.Options;
using Polly;
using Polly.Retry;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace BigSystems.Caseopgave.ParkingService.Facades;

public class CarInformation
{
    [JsonPropertyName("registration_number")]
    public string NummerPlade { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public string Type { get; set; } = string.Empty;
}

public interface IMotorApiFacade
{
    Task<CarInformation> GetVehicleInformation(string numberplate);
}

public class MotorApiOptions
{
    public static string Key => nameof(MotorApiOptions);
    public string Url { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
}

public class MotorApiFacade : IMotorApiFacade
{
    private readonly HttpClient client;
    private readonly AsyncRetryPolicy retryPolicy;

    public MotorApiFacade(HttpClient client, IOptions<MotorApiOptions> options)
    {
        this.client = client;
        client.BaseAddress = new Uri(options.Value.Url);
        client.DefaultRequestHeaders.Add("X-AUTH-TOKEN", options.Value.Token);
        retryPolicy = Policy.Handle<Exception>().RetryAsync();
    }

    public async Task<CarInformation> GetVehicleInformation(string numberplate)
    {
        return await client.GetFromJsonAsync<CarInformation>($"/vehicles/{numberplate}");
    }
}

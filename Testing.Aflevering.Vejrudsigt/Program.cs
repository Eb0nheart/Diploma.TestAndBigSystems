using Microsoft.AspNetCore.Mvc;
using Testing.Aflevering.Vejrudsigt;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddHttpClient()
    .AddFunctionality();

var app = builder.Build();

app.MapGet("/weather", async ([FromServices] IWeatherService weatherService, IDescriptionGenerator descriptionGenerator) =>
{
    var weather = await weatherService.GetTodaysWeather("aabenraa");
    return descriptionGenerator.GenerateDescription(weather);
});

app.Run();

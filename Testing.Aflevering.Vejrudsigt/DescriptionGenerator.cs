namespace Testing.Aflevering.Vejrudsigt;

public interface IDescriptionGenerator
{
    string GenerateDescription(WeatherInfo weather);
}

sealed class DescriptionGenerator : IDescriptionGenerator
{
    private static readonly List<string> validConditions = new()
    {
        "klart vejr",
        "regn",
        "sne",
        "skyet",
        "andet"
    };

    public string GenerateDescription(WeatherInfo weather)
    {
        var temperatureDescription = "";
        var descriptionEnding = "PERFEKT!";

        if(!validConditions.Contains(weather.Conditions.ToLowerInvariant()) || 
            weather.Temperature <= -90 || 
            weather.Temperature > 56)
        {
            throw new InvalidDataException($"Dataene er invalide!");
        }

        if(weather.Temperature > 25)
        {
            temperatureDescription = "varme nok til at slå en elefant ihjel";
            descriptionEnding = "FUCKING LORT!";
        }

        if(weather.Temperature < 25 && weather.Temperature > 20)
        {
            temperatureDescription = "den der irriterende slags varme!";
            descriptionEnding = "meh..";
        }

        if(weather.Temperature < 20)
        {
            temperatureDescription = "temperatur hvor man faktisk kan overleve!!";
        }

        return $"{weather.Conditions} og {temperatureDescription}. {descriptionEnding}";
    }
}
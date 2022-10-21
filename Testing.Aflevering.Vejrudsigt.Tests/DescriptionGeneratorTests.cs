using Microsoft.Extensions.DependencyInjection;

namespace Testing.Aflevering.Vejrudsigt.Tests
{
    public class DescriptionGeneratorTests
    {
        private readonly IServiceProvider serviceProvider;

        public DescriptionGeneratorTests()
        {
            serviceProvider = new ServiceCollection().AddFunctionality().BuildServiceProvider();
        }

        [Fact]
        public void Too_Low_Temperature_Fails()
        {
            var systemUnderTest = serviceProvider.GetRequiredService<IDescriptionGenerator>();
            var weather = new WeatherInfo("Regn", -90);

            var act = Task.Run(() => systemUnderTest.GenerateDescription(weather));

            Assert.ThrowsAsync<InvalidDataException>(async () => await act);
        }

        [Fact]
        public void Too_High_Temperature_Fails()
        {
            var systemUnderTest = serviceProvider.GetRequiredService<IDescriptionGenerator>();
            var weather = new WeatherInfo("Regn", 57);

            var act = Task.Run(() => systemUnderTest.GenerateDescription(weather));

            Assert.ThrowsAsync<InvalidDataException>(async () => await act);
        }

        [Fact]
        public void Incorrect_Conditions_Fails()
        {
            var systemUnderTest = serviceProvider.GetRequiredService<IDescriptionGenerator>();
            var weather = new WeatherInfo("Hej", -89);

            var act = Task.Run(() => systemUnderTest.GenerateDescription(weather));

            Assert.ThrowsAsync<InvalidDataException>(async () => await act);
        }

        [Fact]
        public void Low_Temperature_Rain_Succeeds()
        {
            var systemUnderTest = serviceProvider.GetRequiredService<IDescriptionGenerator>();
            var weather = new WeatherInfo("Regn", -89);

            var result = systemUnderTest.GenerateDescription(weather);

            Assert.Equal("Regn og temperatur hvor man faktisk kan overleve!!. PERFEKT!", result);
        }

        [Fact]
        public void High_Temperature_Rain_Succeeds()
        {
            var systemUnderTest = serviceProvider.GetRequiredService<IDescriptionGenerator>();
            var weather = new WeatherInfo("Regn", 56);

            var result = systemUnderTest.GenerateDescription(weather);

            Assert.Equal("Regn og varme nok til at slå en elefant ihjel. FUCKING LORT!", result);
        }

        [Fact]
        public void Medium_Temperature_Rain_Succeeds()
        {
            var systemUnderTest = serviceProvider.GetRequiredService<IDescriptionGenerator>();
            var weather = new WeatherInfo("Regn", 23);

            var result = systemUnderTest.GenerateDescription(weather);

            Assert.Equal("Regn og den der irriterende slags varme!. meh..", result);
        }
    }
}
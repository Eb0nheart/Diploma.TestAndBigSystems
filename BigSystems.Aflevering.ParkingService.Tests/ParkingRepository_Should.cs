using BigSystems.Caseopgave.ParkingService.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BigSystems.Aflevering.ParkingService.Tests;

public class ParkingRepository_Should
{
    private readonly IServiceProvider provider;

    public ParkingRepository_Should()
	{
        provider = new ServiceCollection()
            .AddTransient<IParkingRepository, ParkingRepository>()
            .BuildServiceProvider();
    }

    [Fact]
    public async Task DeleteAll()
    {
        var systemUnderTest = provider.GetRequiredService<IParkingRepository>();
        var parkings = new List<Parking>
        {
            new Parking(DateTime.Now, "ET12987", "13", "minEmail@hejsa.dk"),
            new Parking(DateTime.Now, "PA09813", "77", "dinEmail@farvel.dk"),
            new Parking(DateTime.Now, "FE78270", "300", "mogenshimmelund@yahoo.net")
        };

        parkings.ForEach(async (parking) => await systemUnderTest.Insert(parking));
        await systemUnderTest.Insert(new Parking(DateTime.Now, "KQ09371", "8", "jens@yahoo.net"));
        await systemUnderTest.DeleteAll(parkings);
        var actual = await systemUnderTest.GetAll();

        Assert.Single(actual);
    }

    [Fact]
    public async Task SavesParking()
    {
        var systemUnderTest = provider.GetRequiredService<IParkingRepository>();
        var expected = new Parking(DateTime.Now, "ET12987", "13", "minEmail@hejsa.dk");

        await systemUnderTest.Insert(expected);
        var actual = (await systemUnderTest.GetAll()).First();

        Assert.Equal(expected, actual);
    }
}
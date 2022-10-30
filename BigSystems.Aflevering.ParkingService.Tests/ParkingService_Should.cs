using BigSystems.Caseopgave.ParkingService.Repositories;
using BigSystems.Caseopgave.ParkingService.Services;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace BigSystems.Aflevering.ParkingService.Tests;

public class ParkingService_Should
{
    private readonly IServiceProvider provider;
    private readonly Mock<IParkingRepository> repositoryMock;
    public ParkingService_Should()
    {
        repositoryMock = new Mock<IParkingRepository>();
        provider = new ServiceCollection()
            .AddTransient<IParkingService, Caseopgave.ParkingService.Services.ParkingService>()
            .AddTransient(_ => repositoryMock.Object)
            .BuildServiceProvider();
    }

    [Fact]
    public async Task DeleteAllRegisteredParkings()
    {
        var systemUnderTest = provider.GetRequiredService<IParkingService>();
        var numberPlate = "CT70915";
        var parkings = new List<Parking>()
            {
                new Parking(DateTime.Now, numberPlate, "13", "minEmail@hejsa.dk"),
                new Parking(DateTime.Now, numberPlate, "77", "dinEmail@farvel.dk"),
                new Parking(DateTime.Now, numberPlate, "300", "mogenshimmelund@yahoo.net")
            };
        repositoryMock
            .Setup(mock => mock.GetAll())
            .Returns(Task.FromResult(parkings));
        repositoryMock.Setup(mock => mock.DeleteAll(parkings));

        await systemUnderTest.DeleteAllRegisteredParkings(numberPlate);

        repositoryMock.Verify(mock => mock.DeleteAll(parkings));
    }

    [Fact]
    public async Task CheckCarRegistration()
    {
        var systemUnderTest = provider.GetRequiredService<IParkingService>();
        var numberPlate = "AZ12548";
        var parkings = new List<Parking>()
            {
                new Parking(DateTime.Now, numberPlate, "13", "endnuen@mojn.dk"),
                new Parking(DateTime.Now, numberPlate, "77", "enanden@jatak.it"),
                new Parking(DateTime.Now, numberPlate, "300", "google@gmail.com")
            };
        repositoryMock
            .Setup(mock => mock.GetAll())
            .Returns(Task.FromResult(parkings));

        var actual = await systemUnderTest.IsCarRegisteredForLot(numberPlate, "13");

        Assert.True(actual);
    }

    [Fact]
    public async Task RegisterParking()
    {
        var systemUnderTest = provider.GetRequiredService<IParkingService>();
        var parking = new Parking(DateTime.Now, "CT70915", "666", "bingbong@bing.net");
        repositoryMock.Setup(mock => mock.Insert(parking));

        await systemUnderTest.RegisterParking(parking);

        repositoryMock.Verify(mock => mock.Insert(parking));
    }
}
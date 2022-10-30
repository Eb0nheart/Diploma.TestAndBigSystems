using BigSystems.Caseopgave.ParkingService.Repositories;

namespace BigSystems.Caseopgave.ParkingService.Services;

public interface IParkingService
{
    Task RegisterParking(Parking parking);

    Task<bool> IsCarRegisteredForLot(string numberPlate, string lot);

    Task DeleteAllRegisteredParkings(string numberPlate);
}

public class ParkingService : IParkingService
{
    private readonly IParkingRepository repository;

    public ParkingService(IParkingRepository repository)
    {
        this.repository = repository;
    }

    public async Task DeleteAllRegisteredParkings(string numberPlate)
    {
        var parkingsToDelete = (await repository.GetAll()).Where(parking => parking.NumberPlate == numberPlate);
        await repository.DeleteAll(parkingsToDelete.ToArray());
    }

    public async Task<bool> IsCarRegisteredForLot(string numberPlate, string lot)
    {
        return (await repository.GetAll()).Any(parking => parking.NumberPlate == numberPlate && parking.Lot == lot);
    }

    public async Task RegisterParking(Parking parking)
    {
        await repository.Insert(parking);
    }
}


namespace BigSystems.Caseopgave.ParkingService.Repositories;

public record Parking(DateTime Parked, string NumberPlate, string Lot, string Email);

public interface IParkingRepository
{
    Task<List<Parking>> GetAll();

    Task Insert(Parking parking);

    Task DeleteAll(IEnumerable<Parking> parkingsToDelete);
}

public class ParkingRepository : IParkingRepository
{
    private readonly List<Parking> parkings;

    public ParkingRepository()
    {
        parkings = new List<Parking>();
    }

    public Task DeleteAll(IEnumerable<Parking> parkingsToDelete)
    {
        foreach (var parking in parkingsToDelete)
        {
            parkings.Remove(parking);
        }
        return Task.CompletedTask;
    }

    public Task<List<Parking>> GetAll() => Task.FromResult(parkings);

    public Task Insert(Parking parking)
    {
        parkings.Add(parking);
        return Task.CompletedTask;
    }
}

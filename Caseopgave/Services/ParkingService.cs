using Caseopgave.CoreFunktionalitet.Models;

namespace Caseopgave.Api.Services;

public interface IParkingService
{
    Task RegisterParking(Parking parking);

    Task<bool> IsCarRegisteredForLot(string numberPlate, string lot);

    Task DeleteAllRegisteredParkings(string numberPlate);
}

sealed class ParkingService : IParkingService
{
    public Task DeleteAllRegisteredParkings(string numberPlate)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsCarRegisteredForLot(string numberPlate, string lot)
    {
        throw new NotImplementedException();
    }

    public Task RegisterParking(Parking parking)
    {
        throw new NotImplementedException();
    }
}


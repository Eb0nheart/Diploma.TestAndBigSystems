﻿namespace BigSystems.Caseopgave.ParkingService.DTO;

public class GetParkingRequest
{
    public string Lot { get; set; }

    public string NumberPlate { get; set; }
}

public class PostParkingRequest
{
    public string Lot { get; set; }

    public string NumberPlate { get; set; }

    public string Email { get; set; }
}
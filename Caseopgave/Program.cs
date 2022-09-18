using Caseopgave.Api.DTO;
using Caseopgave.Api.Services;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IParkingService, ParkingService>();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapGet("/isCarRegistered", async ([FromServices] IParkingService service, [FromBody] GetParkingRequest body) =>
{
    return await service.IsCarRegisteredForLot(body.NumberPlate, body.Lot);
});

app.MapPost("/registerParking", async ([FromServices] IParkingService service, [FromBody] PostParkingRequest body) =>
{
    await service.RegisterParking(new Parking(DateTime.Now, body.NumberPlate, body.Lot));
    return StatusCodes.Status201Created;
});

app.MapDelete("/deleteregistrations/{numberplate}", async ([FromServices] IParkingService service, string numberplate) =>
{
    await service.DeleteAllRegisteredParkings(numberplate);
    return StatusCodes.Status204NoContent;
});

app.Run();
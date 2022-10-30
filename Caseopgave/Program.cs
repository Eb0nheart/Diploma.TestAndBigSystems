using BigSystems.Caseopgave.ParkingService.DTO;
using BigSystems.Caseopgave.ParkingService.Facades;
using BigSystems.Caseopgave.ParkingService.Repositories;
using BigSystems.Caseopgave.ParkingService.Services;
using Microsoft.AspNetCore.Mvc;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder
    .Host
    .ConfigureServices((context,services) =>
    {
        services
        .AddLogging()
            .Configure<MotorApiOptions>((options) => context.Configuration.GetSection(MotorApiOptions.Key).Bind(options))
            .AddTransient<IParkingService, ParkingService>()
            .AddSingleton<IParkingRepository, ParkingRepository>()
            .AddTransient<IMotorApiFacade, MotorApiFacade>()
            .AddHttpClient(); 
    })
    .UseSerilog((context, configuration) =>
    {
        configuration.WriteTo.Console();
    });

var app = builder.Build();

app.UseHttpsRedirection();

app.MapGet("/isCarRegistered", async ([FromServices] IParkingService service, [FromBody] GetParkingRequest body) =>
{
    return await service.IsCarRegisteredForLot(body.NumberPlate, body.Lot);
});

app.MapPost("/registerParking", async ([FromServices] IParkingService service, [FromBody] PostParkingRequest body) =>
{
    await service.RegisterParking(new Parking(DateTime.Now, body.NumberPlate, body.Lot, body.Email));
    return StatusCodes.Status201Created;
});

app.MapDelete("/deleteregistrations/{numberplate}", async ([FromServices] IParkingService service, string numberplate) =>
{
    await service.DeleteAllRegisteredParkings(numberplate);
    return StatusCodes.Status204NoContent;
});

app.Run();
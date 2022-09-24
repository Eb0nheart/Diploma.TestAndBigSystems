using Caseopgave.Api.DTO;
using Caseopgave.Api.Services;
using Caseopgave.CoreFunktionalitet;
using Microsoft.AspNetCore.Mvc;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder
    .Host
    .ConfigureServices((context,services) =>
    {
        services
        .AddLogging()
            .AddCoreFunctionality((options) => context.Configuration.GetSection(MotorApiOptions.Key).Bind(options))
            .AddTransient<IParkingService, ParkingService>()
            .AddHttpClient(); 
    })
    .UseSerilog((context, configuration) =>
    {
        configuration.WriteTo.Console();
    });

var app = builder.Build();

app.UseHttpsRedirection();

app.MapGet("/", () => "Hello World!");

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
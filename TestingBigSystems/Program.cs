using Microsoft.AspNetCore.Mvc;
using TestingBigSystems.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<ICardsRepository, CardsRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();


app.MapGet("/card", ([FromServices] ICardsRepository Repository) =>
{
    return Repository.GetCard().DisplayName;
});

app.Run();

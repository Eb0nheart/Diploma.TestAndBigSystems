namespace BigSystems.Caseopgave.EventService.Models;

public record Event(Guid OrderId, DateTime Timestamp, uint TableNumber, uint PizzaNumber);

namespace Caseopgave.EventService.DataAccess;

public interface IEventsRepository
{
    Task<List<Event>> GetEventsAsync();

    Task InsertEventAsync(Event data);
}

sealed class EventsRepository : IEventsRepository
{
    private List<Event> Repository { get; }

    private readonly ILogger<EventsRepository> logger;

    public EventsRepository(
        ILogger<EventsRepository> logger)
    {
        this.logger = logger;
        Repository = new List<Event>();
    }

    public Task<List<Event>> GetEventsAsync()
    {
        return Task.FromResult(Repository);
    }

    public Task InsertEventAsync(Event data)
    {
        if (Repository.Contains(data))
        {
            logger.LogWarning("Event already exists");
            return Task.CompletedTask;
        }

        Repository.Add(data);
        return Task.CompletedTask;
    }
}

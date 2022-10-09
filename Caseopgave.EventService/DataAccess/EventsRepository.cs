namespace Caseopgave.EventService.DataAccess;

public class IndexDTO
{
    public int From { get; set; } = -1;

    public int Amount { get; set; } = 0;
}

public interface IEventsRepository
{
    Task<List<Event>> GetEventsAsync(IndexDTO index);

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

    public Task<List<Event>> GetEventsAsync(IndexDTO index)
    {
        if(index.From > index.Amount)
        {
            throw new InvalidOperationException("To needs to be higher than from!");
        }

        if(index.From != -1 && index.Amount != 00)
        {
            return Task.FromResult(Repository.GetRange(index.From, index.Amount));
        }

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

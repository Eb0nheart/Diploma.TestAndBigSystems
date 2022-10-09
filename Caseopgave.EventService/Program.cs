using Caseopgave.EventService.DataAccess;
using Caseopgave.EventService.Services;
using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host
    .ConfigureServices((context, services) =>
    {
        services
            .Configure<EventSubscriberOptions>(options => context.Configuration.GetSection(EventSubscriberOptions.Key).Bind(options))
            .AddTransient<IDeserializer<Event>, EventDeserializer>()
            .AddSingleton<IEventsRepository, EventsRepository>()
            .AddSingleton(provider =>
            {
                var config = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("auto.offset.reset", "latest"),
                    new KeyValuePair<string, string>("group.id", "pizza-orders-consumers"),
                    new KeyValuePair<string, string>("bootstrap.servers", "localhost:9092")
                };
                var consumerBuilder = new ConsumerBuilder<Null, Event>(config.AsEnumerable());
                consumerBuilder.SetValueDeserializer(provider.GetRequiredService<IDeserializer<Event>>());
                return consumerBuilder.Build();
            })
            .AddHostedService<EventSubscriber>();
    })
    .UseSerilog((_, configuration) =>
    {
        configuration.WriteTo.Console(Serilog.Events.LogEventLevel.Debug);
    });

var app = builder.Build();

app.UseHttpsRedirection();

app.MapGet("/events", async ([FromServices] IEventsRepository repository) =>
{
    return await repository.GetEventsAsync();
});

await app.RunAsync();
using BigSystems.Caseopgave.EventService.DataAccess;
using BigSystems.Caseopgave.EventService.Models;
using Confluent.Kafka;
using Microsoft.Extensions.Options;

namespace BigSystems.Caseopgave.EventService.Services
{
    public class EventSubscriberOptions
    {
        public static string Key => nameof(EventSubscriberOptions);

        public double PollIntervalMinutes { get; set; } = 1;

        public string QueueName { get; set; } = string.Empty;
    }

    public class EventSubscriber : BackgroundService
    {
        private readonly ILogger<EventSubscriber> logger;
        private readonly IConsumer<Null, Event> consumer;
        private readonly IEventsRepository repository;
        private readonly EventSubscriberOptions options;
        private readonly PeriodicTimer timer;

        public EventSubscriber(
            ILogger<EventSubscriber> logger,
            IOptions<EventSubscriberOptions> options,
            IConsumer<Null, Event> consumer,
            IEventsRepository repository)
        {
            this.logger = logger;
            this.consumer = consumer;
            this.repository = repository;
            this.options = options.Value;
            timer = new PeriodicTimer(TimeSpan.FromMinutes(this.options.PollIntervalMinutes));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation("Subscribing to Events");
            consumer.Subscribe(options.QueueName);

            while (await timer.WaitForNextTickAsync(stoppingToken))
            {
                var result = consumer.Consume(stoppingToken);
                await repository.InsertEventAsync(result.Message.Value);
            }
        }
    }
}

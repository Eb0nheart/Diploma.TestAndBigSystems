using BigSystems.Caseopgave.EventService.Models;
using Confluent.Kafka;
using System.Text;

namespace BigSystems.Caseopgave.EventService.Services;

public class EventDeserializer : IDeserializer<Event>
{
    public Event Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
    {
        return JsonConvert.DeserializeObject<Event>(Encoding.UTF8.GetString(data));
    }
}